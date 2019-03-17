using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class PlatformerCharacter2D : NetworkBehaviour
{
    [SerializeField]
    private WaitForSeconds stunDuration = new WaitForSeconds(3.0f);
    [HideInInspector]
    public float m_MaxSpeed = 0f;                    // The fastest the player can travel.

    private Animator m_Anim;            // Reference to the player's animator component.
    public Rigidbody2D m_Rigidbody2D;
    private bool isStunned;
    private bool isDead;
    public bool isEnabled = true;
    private bool isDashing = false;
    private float currDash;
    private float dashDur = 0.25f;

    public bool IsDead
    {
        get { return isDead; }
    }
    private void Awake()
    {
        // Setting up references.
        m_Anim = GetComponent<Animator>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        this.isStunned = false;
    }

    public void Move(float moveH, float moveV)
    {
        // Move the character
        if (!isStunned && !isDead && isEnabled && !isDashing)
        {
            m_Rigidbody2D.velocity = new Vector2(moveH * m_MaxSpeed, moveV * m_MaxSpeed);
        }

        if(isDashing && Time.time - currDash > dashDur)
        {
            isDashing = false;
            m_Rigidbody2D.velocity = new Vector2();
        }
    }

    public void FaceMouse(Vector3 pointToFace)
    {
        if (isEnabled)
        {
            Vector2 direction = new Vector2(pointToFace.x - transform.position.x, pointToFace.y - transform.position.y);
            //transform.right = direction;

            float angle = Vector2.SignedAngle(Vector2.right, direction);
            m_Rigidbody2D.MoveRotation(angle);
        }
    }

    public IEnumerator StunPlayer()
    {
        this.isStunned = true;
        yield return stunDuration;
        this.isStunned = false;
    }

    public void Unstun()
    {
        this.isStunned = false;
    }

    public void Kill(bool condition)
    {
        this.isDead = condition;
    }

    public void Dash()
    {
        m_Rigidbody2D.velocity = new Vector2();
        m_Rigidbody2D.AddForce(30 * transform.right, ForceMode2D.Impulse);
        this.isDashing = true;
        this.currDash = Time.time;
    }

    public void AbilityOnePressed()
    {
        // for each ability1 we have, call button pressed on it
        if (GetComponent<BasePlayer>() != null)
        {
            List<GameObject> abilities = GetComponent<BasePlayer>().abilities;
            int i = 0;
            foreach (GameObject a in abilities)
            {
                Ability ability = a.GetComponent<Ability>();
                if (ability.abilityButton == "Fire1")
                {
                    CmdFireAbilityOne(i);
                }
                i++;
            }
        }
    }
    public void AbilityTwoPressed()
    {
        // for each ability1 we have, call button pressed on it
        if (GetComponent<BasePlayer>() != null)
        {
            List<GameObject> abilities = GetComponent<BasePlayer>().abilities;
            int i = 0;
            foreach (GameObject a in abilities)
            {
                Ability ability = a.GetComponent<Ability>();
                if (ability.abilityButton == "Fire2")
                {
                    CmdFireAbilityTwo(i);
                }
                i++;
            }
        }
    }

    public void AbilityOneReleased()
    {
        // for each ability1 we have, call button pressed on it
        if (GetComponent<BasePlayer>() != null)
        {
            List<GameObject> abilities = GetComponent<BasePlayer>().abilities;
            int i = 0;
            foreach (GameObject a in abilities)
            {
                Ability ability = a.GetComponent<Ability>();
                if (ability.abilityButton == "Fire1")
                {
                    CmdReleaseAbilityOne(i);
                }
                i++;
            }
        }
    }

    [Command]
    public void CmdSpawnAbilities(string[] fixedAbilities)
    {
        if (connectionToClient.isReady)
        {
            Spawn(fixedAbilities);
        }
        else
        {
            StartCoroutine(WaitForReady(fixedAbilities));
        }
    }

    IEnumerator WaitForReady(string[] fixedAbilities)
    {
        while (!connectionToClient.isReady)
        {
            yield return new WaitForSeconds(0.25f);
        }
        Spawn(fixedAbilities);
    }
    [Server]
    void Spawn(string[] fixedAbilities)
    {
        foreach (string ability in fixedAbilities)
        {
            GameObject abilityObject = (GameObject)Resources.Load(ability, typeof(GameObject));
            GameObject instance = Instantiate(abilityObject);//, transform);
            instance.GetComponent<Ability>().ownerNetworkId = netId;
            NetworkServer.SpawnWithClientAuthority(instance, connectionToClient);
            //abilities.Add(instance);
        }
        Debug.Log("Successfully spawned abilities");
    }

    [Command]
    public void CmdFireAbilityOne(int i)
    {
        RpcFireAbilityOne(i);
    }
    [ClientRpc]
    public void RpcFireAbilityOne(int i)
    {
        Debug.Log("ABILITY 1 FIRED");
        Ability a = GetComponent<BasePlayer>().abilities[i].GetComponent<Ability>();
        a.OnButtonDown();
    }

    [Command]
    public void CmdFireAbilityTwo(int i)
    {
        RpcFireAbilityTwo(i);
    }
    [ClientRpc]
    public void RpcFireAbilityTwo(int i)
    {
        Debug.Log("ABILITY 2 FIRED");
        Ability a = GetComponent<BasePlayer>().abilities[i].GetComponent<Ability>();
        a.Fire();
    }

    [Command]
    public void CmdReleaseAbilityOne(int i)
    {
        RpcReleaseAbilityOne(i);
    }
    [ClientRpc]
    public void RpcReleaseAbilityOne(int i)
    {
        Debug.Log("ABILITY 1 released");
        Ability a = GetComponent<BasePlayer>().abilities[i].GetComponent<Ability>();
        a.OnButtonRelease();
    }

}

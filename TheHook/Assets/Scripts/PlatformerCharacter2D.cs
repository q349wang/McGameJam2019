using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

// this class should only have movement functions, not player state
public class PlatformerCharacter2D : NetworkBehaviour
{
    [SerializeField]
    private WaitForSeconds stunDuration = new WaitForSeconds(3.0f);
    [HideInInspector]
    public float m_MaxSpeed = 0f;                    // The fastest the player can travel.

    private Animator m_Anim;            // Reference to the player's animator component.
    public Rigidbody2D m_Rigidbody2D;
    private bool isStunned;
    public bool isEnabled = true;
    private bool isDashing = false;
    private float currDash;
    private float dashDur = 0.25f;

    
    private void Awake()
    {
        // Setting up references.
        m_Anim = GetComponent<Animator>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        this.isStunned = false;
    }

    // movement functions should only be called from fixedupdate
    public void Move(float moveH, float moveV)
    {
        // Move the character
        if (!isStunned && isEnabled && !isDashing)
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

    public void Dash()
    {
        m_Rigidbody2D.velocity = new Vector2();
        m_Rigidbody2D.AddForce(30 * transform.right, ForceMode2D.Impulse);
        this.isDashing = true;
        this.currDash = Time.time;
    }


    ///// Using Abilities /////

    public void AbilityPressed(string binding)
    {
        // for each ability we have, call button pressed on it
        if (GetComponent<BasePlayer>() != null)
        {
            int i = 0;
            foreach (Ability ability in GetComponent<BasePlayer>().abilities)
            {
                if (ability.abilityButton == binding)
                {
                    CmdFireAbility(i);
                }
                i++;
            }
        }
    }

    public void AbilityReleased(string binding)
    {
        // for each ability we have, call button released on it
        if (GetComponent<BasePlayer>() != null)
        {
            int i = 0;
            foreach (Ability ability in GetComponent<BasePlayer>().abilities)
            {
                if (ability.abilityButton == binding)
                {
                    CmdReleaseAbilityOne(i);
                }
                i++;
            }
        }
    }

    // called from the local player
    [Command]
    public void CmdFireAbility(int i)
    {
        RpcFireAbility(i);
    }
    [ClientRpc]
    public void RpcFireAbility(int i)
    {
        Ability a = GetComponent<BasePlayer>().abilities[i];
        a.OnButtonDown();
    }

    [Command]
    public void CmdReleaseAbilityOne(int i)
    {
        RpcReleaseAbilityOne(i);
    }
    [ClientRpc]
    public void RpcReleaseAbilityOne(int i)
    {
        Ability a = GetComponent<BasePlayer>().abilities[i];
        a.OnButtonRelease();
    }

}

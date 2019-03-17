using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

[RequireComponent(typeof(NetworkIdentity))]
public abstract class Ability : NetworkBehaviour
{

    public string aName = "New Ability";
    public string abilityButton = "Fire1";
    public Sprite aSprite;
    public float abCoolDown = 1f;
    public int abCost = 20;
    protected float nextReadyTime;
    private float coolDownTimeLeft;
    protected BasePlayer bPlayer;
    public NetworkInstanceId ownerNetworkId;

    protected bool onCooldown = false;

    public virtual void Start()
    {
        Object[] objects = FindObjectsOfType<PlatformerCharacter2D>();
        Debug.Log("Found " + objects.Length + " objects");
        foreach (Object obj in objects)
        {
            PlatformerCharacter2D gObj = obj as PlatformerCharacter2D;
            if (gObj.netId == ownerNetworkId)
            {
                transform.SetParent(gObj.transform);
                bPlayer = gObj.GetComponent<BasePlayer>();
            }
        }
        bPlayer = transform.GetComponentInParent<BasePlayer>();
        AbilityReady();
    }

    public virtual void OnButtonDown()
    {
        Fire();
    }

    public virtual void OnButtonRelease()
    {
        Release();
    }

    protected virtual void Update()
    {
        //bool coolDownComplete = (Time.time > nextReadyTime);
        //onCooldown = !coolDownComplete;
        //if (coolDownComplete)
        //{
        //    AbilityReady();
        //    if(abCoolDown > 0)
        //    {
        //        if (Input.GetButtonDown(abilityButton) && bPlayer.CurrentMana >= abCost)
        //        {
        //            ButtonTriggered();
        //        }
        //    }
        //    else
        //    {
        //        if(Input.GetButton(abilityButton) && bPlayer.CurrentMana >= abCost)
        //        {
        //            ButtonTriggered();
        //        }
        //    } 
        //}
        //else
        //{
        //    CoolDown();
        //}
    }

    public bool AbilityReady()
    {
        return bPlayer.CurrentMana >= abCost;
        //coolDownTextDisplay.enabled = false;
        //darkMask.enabled = false;
    }

    protected void CoolDown()
    {
        coolDownTimeLeft -= Time.deltaTime;
        float roundedCd = Mathf.Round(coolDownTimeLeft);
        //coolDownTextDisplay.text = roundedCd.ToString();
        //darkMask.fillAmount = (coolDownTimeLeft / coolDownDuration);
    }

    protected void ButtonTriggered()
    {
        nextReadyTime = abCoolDown + Time.time;
        coolDownTimeLeft = abCoolDown;
        //darkMask.enabled = true;
        //coolDownTextDisplay.enabled = true;

        Fire();
    }

    public abstract void Fire();
    public abstract void Release();
}
using UnityEngine;
using System.Collections;

public abstract class Ability : MonoBehaviour
{

    public string aName = "New Ability";
    public string abilityButton = "Fire1";
    public Sprite aSprite;
    public float abCoolDown = 1f;
    public int abCost = 20;
    private float nextReadyTime;
    private float coolDownTimeLeft;
    private BasePlayer bPlayer;

    public void Start()
    {
        bPlayer = transform.GetComponentInParent<BasePlayer>();
        AbilityReady();
    }

    private void Update()
    {
        bool coolDownComplete = (Time.time > nextReadyTime);
        if (coolDownComplete)
        {
            AbilityReady();
            if(abCoolDown > 0)
            {
                if (Input.GetButtonDown(abilityButton) && bPlayer.CurrentMana >= abCost)
                {
                    ButtonTriggered();
                }
            }
            else
            {
                if(Input.GetButton(abilityButton) && bPlayer.CurrentMana >= abCost)
                {
                    ButtonTriggered();
                }
            } 
        }
        else
        {
            CoolDown();
        }
    }

    private void AbilityReady()
    {
        //coolDownTextDisplay.enabled = false;
        //darkMask.enabled = false;
    }

    private void CoolDown()
    {
        coolDownTimeLeft -= Time.deltaTime;
        float roundedCd = Mathf.Round(coolDownTimeLeft);
        //coolDownTextDisplay.text = roundedCd.ToString();
        //darkMask.fillAmount = (coolDownTimeLeft / coolDownDuration);
    }

    private void ButtonTriggered()
    {
        nextReadyTime = abCoolDown + Time.time;
        coolDownTimeLeft = abCoolDown;
        //darkMask.enabled = true;
        //coolDownTextDisplay.enabled = true;

        Fire();
    }

    public abstract void Fire();
}
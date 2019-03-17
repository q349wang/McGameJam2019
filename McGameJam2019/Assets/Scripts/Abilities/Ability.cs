//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public abstract class Ability : MonoBehaviour
//{
//    [SerializeField] private Transform m_Ability1;
//    [SerializeField] private Transform m_Ability2;
//    [SerializeField] private Transform m_Ability3;

//    [SerializeField] public int cost;
//    [SerializeField] public int coolDown;
//    [SerializeField] private int characterClass;

//    private void Awake()
//    {
//        m_Ability1 = transform.Find("Ability 1");
//        m_Ability2 = transform.Find("Ability 2");
//        m_Ability3 = transform.Find("Ability 3");
//    }

//}

using UnityEngine;
using System.Collections;

public abstract class Ability : MonoBehaviour
{

    public string aName = "New Ability";
    public string abilityButton = "Fire1";
    public Sprite aSprite;
    public float abCoolDown = 1f;
    public int abCost = 20;
    protected float nextReadyTime;
    private float coolDownTimeLeft;
    protected BasePlayer bPlayer;

    protected bool onCooldown = false;

    public virtual void Start()
    {
        bPlayer = transform.GetComponentInParent<BasePlayer>();
        AbilityReady();
    }

    protected virtual void Update()
    {
        bool coolDownComplete = (Time.time > nextReadyTime);
        onCooldown = !coolDownComplete;
        if (coolDownComplete)
        {
            AbilityReady();
            if (Input.GetButtonDown(abilityButton) && bPlayer.CurrentMana >= abCost)
            {
                ButtonTriggered();
            }
        }
        else
        {
            CoolDown();
        }
    }

    protected virtual void AbilityReady()
    {
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
}
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
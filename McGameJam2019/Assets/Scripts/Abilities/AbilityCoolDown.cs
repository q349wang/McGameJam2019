using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEditor;

public class AbilityCoolDown : MonoBehaviour
{

    public string abilityButtonAxisName = "Fire1";
    public Image darkMask;
    public Text coolDownTextDisplay;

    [SerializeField] private GameObject abilityObject;
    private Ability ability;
    [SerializeField] private GameObject weaponHolder;
    private Image myButtonImage;
    private float coolDownDuration;
    private float nextReadyTime;
    private float coolDownTimeLeft;


    void Start()
    {
        ability = abilityObject.GetComponent<Ability>();
        Initialize(ability, weaponHolder);
    }

    public void Initialize(Ability selectedAbility, GameObject weaponHolder)
    {
        ability = selectedAbility;
        //myButtonImage = GetComponent<Image>();
        //myButtonImage.sprite = ability.aSprite;
        //darkMask.sprite = ability.aSprite;
        coolDownDuration = ability.abCoolDown;
        //ability.Initialize(weaponHolder);
        AbilityReady();
    }

    // Update is called once per frame
    void Update()
    {
        bool coolDownComplete = (Time.time > nextReadyTime);
        if (coolDownComplete)
        {
            AbilityReady();
            if (Input.GetButton(abilityButtonAxisName))
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
        nextReadyTime = coolDownDuration + Time.time;
        coolDownTimeLeft = coolDownDuration;
        //darkMask.enabled = true;
        //coolDownTextDisplay.enabled = true;

        //ability.TriggerAbility();
    }

    public void SetAbility(Ability abil)
    {
        this.ability = abil;
    }
}
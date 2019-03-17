using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DPS : BasePlayer
{
    public Sprite dpsSprite;

    protected override void Start()
    {
        fixedAbilities = new string[] { };// "DPSAbilities" };
        movementSpeed = 6;
        base.Start();    
    }

    public void EquipGun(GameObject gunAbility)
    {
        base.AddAbility(gunAbility);
    }

    public void SetCurrentAmmo(int amount)
    {
        this.mana = amount;
    }
}

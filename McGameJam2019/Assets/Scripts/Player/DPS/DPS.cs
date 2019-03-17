using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DPS : BasePlayer
{
    public Sprite dpsSprite;

    protected override void Start()
    {
        fixedAbilities = new string[] { "DPSAbilities" };
        movementSpeed = 6;
        base.Start();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCurrentAmmo(int amount)
    {
        this.mana = amount;
    }
}

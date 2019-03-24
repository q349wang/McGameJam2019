using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DPS : BasePlayer
{
    public Sprite dpsSprite;

    protected override void Start()
    {
        movementSpeed = 6;
        base.Start();    
    }

    public void EquipGun(GameObject gunAbility)
    {
        // dequip any equipped guns
        abilities.ForEach(ability => Destroy(ability.gameObject));
        abilities.RemoveAll(ability => ability is ProjectileAbility);

        GameObject instance = AddAbility(gunAbility);
        instance.transform.localPosition = new Vector3(0.44f, -0.3f, 0);
        instance.transform.up = transform.right;
    }

    public void SetCurrentAmmo(int amount)
    {
        this.mana = amount;
    }
}

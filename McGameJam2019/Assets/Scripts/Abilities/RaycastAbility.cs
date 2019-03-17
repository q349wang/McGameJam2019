using UnityEngine;
using System.Collections;

public abstract class RaycastAbility : Ability
{

    public int gunDamage = 1;
    public float weaponRange = 50f;
    public float hitForce = 100f;
    public Color laserColor = Color.white;

    //public override void Initialize(GameObject obj)
    //{
    //    rcShoot = obj.GetComponent<RaycastShootTriggerable>();
    //    rcShoot.Initialize();

    //    rcShoot.gunDamage = gunDamage;
    //    rcShoot.weaponRange = weaponRange;
    //    rcShoot.hitForce = hitForce;
    //    rcShoot.cost = this.aBaseCost;
    //    rcShoot.laserLine.material = new Material(Shader.Find("Unlit/Color"));
    //    rcShoot.laserLine.material.color = laserColor;

    //}

}
using UnityEngine;
using System.Collections;

public abstract class ProjectileAbility : Ability
{
    protected int weaponRange;
    protected int damage;
    public float projectileForce = 500f;
    public Rigidbody2D projectile;


    //public override void Initialize(GameObject obj)
    //{
    //    launcher = obj.GetComponent<ProjectileShootTriggerable>();
    //    launcher.projectileForce = projectileForce;
    //    launcher.projectile = projectile;
    //}

    //public override void TriggerAbility()
    //{
    //    launcher.Launch();
    //}


    public int getWeaponRange()
    {
        return weaponRange;
    }

}
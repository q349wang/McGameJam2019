﻿using UnityEngine;
using System.Collections;

public class ProjectileAbility : Ability
{

    public float projectileForce = 500f;
    public Rigidbody projectile;


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

    public override void Fire()
    {

    }

}
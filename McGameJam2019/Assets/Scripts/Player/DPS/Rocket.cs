using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : ProjectileAbility
{
    public GameObject rocket;

    public void Start()
    {
        base.Start();
        rocket = projectile.gameObject;
    }
    public override void Fire()
    {
        Instantiate(rocket, transform.position, Quaternion.identity);
    }

    public override void Release()
    {

    }
}

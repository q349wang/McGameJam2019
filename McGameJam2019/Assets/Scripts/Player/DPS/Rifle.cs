using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : ProjectileAbility
{
    public GameObject bullet;

    public void Start()
    {
        base.Start();
        bullet = projectile.gameObject;
    }
    public override void Fire()
    {
        Debug.Log("Firing");
        Instantiate(bullet, transform.position, transform.rotation);
    }

    public override void Release()
    {

    }
}

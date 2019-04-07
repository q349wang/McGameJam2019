using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : ProjectileAbility
{
    public GameObject bullet;
    public Transform nozzle;

    /// seconds between each fire
    [SerializeField]
    float fireRate = 0.1f;
    float nextFireTime;

    [SerializeField]
    [Range(0.01f, 1f)]
    float slowFactor = 1f;

    bool heldDown = false;

    public void Start()
    {
        base.Start();
        bullet = projectile.gameObject;
    }

    void Update()
    {
        if (heldDown && AbilityReady())
        {
            if (Time.time >= nextFireTime)
            {
                Fire();
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    public override void OnButtonDown()
    {
        if (AbilityReady())
        {
            heldDown = true;
            bPlayer.gameObject.GetComponent<PlatformerCharacter2D>().m_MaxSpeed *= slowFactor;
        }
    }

    public override void Fire()
    {
        ProjectileBehavior bulletInstance = Instantiate(bullet, nozzle.position, transform.rotation).GetComponent<ProjectileBehavior>();
        bPlayer.ServerUseMana(abCost);
    }

    public override void OnButtonRelease()
    {
        if (heldDown)
        {
            heldDown = false;
            bPlayer.gameObject.GetComponent<PlatformerCharacter2D>().m_MaxSpeed /= slowFactor;
        }
    }
}

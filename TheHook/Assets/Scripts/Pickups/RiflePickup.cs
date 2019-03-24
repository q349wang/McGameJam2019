using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Pickups
{
    public class RiflePickup : BasePickup
    {
        [SerializeField]
        private int ammo = 40;

        [SerializeField]
        GameObject rifleAbility;

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            base.OnTriggerEnter2D(other);
            //string otherType = other.gameObject.GetType().ToString();
            //Debug.Log(otherType);
            DPS dps = other.gameObject.GetComponent<DPS>();
            if (dps != null)
            {
                dps.SetCurrentAmmo(ammo);
                dps.EquipGun(rifleAbility);
            }
            else
            {
            }
        }
    }
}

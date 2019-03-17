using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Pickups
{
    public class RiflePickup : BasePickup
    {
        [SerializeField]
        private int ammo = 40;
        protected override void OnTriggerEnter2D(Collider2D other)
        {
            base.OnTriggerEnter2D(other);
            //string otherType = other.gameObject.GetType().ToString();
            //Debug.Log(otherType);
            DPS dps = other.gameObject.GetComponent<DPS>();
            if (dps != null)
            {
                Debug.Log("DPS");
                dps.SetCurrentAmmo(ammo);

            }
            else
            {
                Debug.Log("Not DPS");
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Pickups
{
    public class RiflePickup : BasePickup
    {
        protected override void OnTriggerEnter2D(Collider2D other)
        {
            base.OnTriggerEnter2D(other);
        }
    }
}

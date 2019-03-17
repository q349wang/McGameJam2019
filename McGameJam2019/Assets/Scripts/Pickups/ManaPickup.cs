using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pickups
{

    public class ManaPickup : BasePickup
    {
        public int manaToAdd;
        void OnTriggerEnter(Collider other)
        {
            //player = other.gameObject.GetComponent<HealerAbility1>
        }

    }
}

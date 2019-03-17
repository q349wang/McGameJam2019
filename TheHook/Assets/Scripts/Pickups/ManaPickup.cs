using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pickups
{

    public class ManaPickup : BasePickup
    {
        public int manaToAdd;
        protected override void OnTriggerEnter2D(Collider2D other)
        {
            base.OnTriggerEnter2D(other);
            if(other.gameObject.tag == "Player")
            {
                BasePlayer bp = other.gameObject.GetComponent<BasePlayer>();
                if(bp != null)
                {
                    bp.addMana(manaToAdd);
                }
            }
            else if(other.gameObject.layer == 8) // Hook
            {
                BasePlayer bp = other.gameObject.GetComponent<HookUtils.Hook>().GetPlayer();
                if(bp != null)
                {
                    bp.addMana(manaToAdd);
                }
            }
        }

    }
}

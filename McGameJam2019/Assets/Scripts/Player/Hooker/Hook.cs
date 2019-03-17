using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HookUtils
{
    public class Hook : ProjectileAbility
    {
        public Vector2 offset;
        private bool hasFired;
        // Start is called before the first frame update
        private TrailRenderer trail;
        public override void Start()
        {
            base.Start();
            aName = "Hook";
            abCoolDown = 5f;
            abCost = 20;
            damage = 20;
            weaponRange = 10;
            projectileForce = 15f;
            projectile = GetComponent<Rigidbody2D>();
            projectile.simulated = false;
            transform.localPosition = offset;
            trail = GetComponent<TrailRenderer>();
        }

        // Update is called once per frame
        protected override void Update()
        {
            onCooldown = Time.time <= nextReadyTime;
            if (!onCooldown)
            {
                AbilityReady();
                if (Input.GetButtonDown(abilityButton) && IsAbilityReady())
                {
                    ButtonTriggered();
                }
            }
            else
            {
                CoolDown();
            }
        }

        public bool IsAbilityReady()
        {
            return bPlayer.CurrentMana >= abCost && !hasFired;
        }

        public override void Fire()
        {
            //trail.enabled = true;
            projectile.simulated = true;
            projectile.AddForce(transform.right * projectileForce, ForceMode2D.Impulse);
            hasFired = true;
        }
        public bool isFired()
        {
            return hasFired;
        }
        public void Hit()
        {
            projectile.simulated = false;
            hasFired = false;
        }

        public int getDamage()
        {
            return damage;
        }
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HookUtils
{
    public class Hook : ProjectileAbility
    {
        public Vector3 offset;
        private bool hasFired;
        private float hookDur = 1.5f;
        private float currHook;

        // Start is called before the first frame update

        public override void Start()
        {
            // base.Start();

            aName = "Hook";
            abCoolDown = 2f;
            abCost = 10;
            damage = 40;
            weaponRange = 10;
            projectileForce = 20f;
            projectile = GetComponent<Rigidbody2D>();
            projectile.simulated = false;
            if (bPlayer != null)
            {
                transform.position = Quaternion.Euler(GetPlayer().transform.eulerAngles) * offset + GetPlayer().transform.position;

                transform.rotation = GetPlayer().transform.rotation;
            }

            nextReadyTime = Time.time;
        }

        // Update is called once per frame
        protected override void Update()
        {
            if (!hasFired)
            {
                if (bPlayer != null)
                {
                    transform.position = Quaternion.Euler(GetPlayer().transform.eulerAngles) * offset + GetPlayer().transform.position;

                    transform.rotation = GetPlayer().transform.rotation;
                }

            }
            else if (Time.time - currHook > hookDur)
            {
                Hit();
                HitDone();
                transform.position = Quaternion.Euler(GetPlayer().transform.eulerAngles) * offset + GetPlayer().transform.position;
                transform.rotation = GetPlayer().transform.rotation;
            }
            onCooldown = Time.time < nextReadyTime;
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
            if (bPlayer == null) return false;
            return bPlayer.CurrentMana >= abCost && !hasFired;
        }
        public override void OnButtonDown()
        {
            Fire();
        }
        public override void Fire()
        {
            if (bPlayer != null)
            {   
                bPlayer.castAbility(abCost);
                Debug.Log("Fired");
                hasFired = true;
                bPlayer.SetControl(false);
                projectile.simulated = true;
                Vector2 dir = new Vector2(bPlayer.transform.right.x, bPlayer.transform.right.y);
                projectile.velocity = new Vector2();
                projectile.AddForce(dir.normalized * projectileForce, ForceMode2D.Impulse);
                currHook = Time.time;
            }
        }
        public bool isFired()
        {
            return hasFired;
        }
        public void Hit()
        {
            projectile.simulated = false;

        }

        public void HitDone()
        {
            if (bPlayer != null)
            {
                Debug.Log("Hit Done");
                bPlayer.SetControl(true);
                hasFired = false;
            }
        }

        public int getDamage()
        {
            return damage;
        }

        public override void Release()
        {
        }
        public BasePlayer GetPlayer()
        {
            return bPlayer;
        }

        public void SetPlayer(BasePlayer player)
        {
            bPlayer = player;
            Start();
        }
    }

}
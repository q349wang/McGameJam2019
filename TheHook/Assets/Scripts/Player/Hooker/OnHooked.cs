using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HookUtils
{
    public class OnHooked : MonoBehaviour
    {
        private Collider2D m_Collider2;
        private Hook hookProjectile;

        private bool hooked = false;
        private int hookedDude = -1;
        private GameObject dude;

        private float pullDur = 1.5f;
        private float currentDur;
        private Vector3 attached;
        // Start is called before the first frame update
        void Start()
        {
            m_Collider2 = GetComponent<Collider2D>();
            m_Collider2.isTrigger = false;
            hookProjectile = GetComponent<Hook>();
        }

        // Update is called once per frame
        void Update()
        {

            if (hookProjectile != null && hooked)
            {
                switch (hookedDude)
                {
                    case -1:
                        hookProjectile.Hit();
                        hookProjectile.HitDone();
                        hookProjectile.transform.position = Quaternion.Euler(hookProjectile.GetPlayer().transform.eulerAngles) * hookProjectile.offset + hookProjectile.GetPlayer().transform.position;

                        hookProjectile.transform.rotation = hookProjectile.GetPlayer().transform.rotation;
                        hooked = false;
                        break;
                    case 0:
                        if (dude != null)
                        {
                            hookProjectile.Hit();
                            dude.GetComponent<BasePlayer>().rigidBody.velocity = new Vector2();
                            dude.GetComponent<BasePlayer>().rigidBody.AddForce((hookProjectile.GetPlayer().transform.position - dude.transform.position).normalized * 12, ForceMode2D.Impulse);
                            attached = hookProjectile.transform.position - dude.transform.position;
                            hookedDude = 3;
                        }

                        break;
                    case 1:
                        hookProjectile.Hit();
                        hookProjectile.GetPlayer().rigidBody.AddForce((hookProjectile.transform.position - hookProjectile.GetPlayer().transform.position).normalized * 12, ForceMode2D.Impulse);
                        hookedDude = 2;
                        break;
                    case 2:
                        if ((hookProjectile.transform.position - hookProjectile.GetPlayer().transform.position).magnitude < 1 || Time.time - currentDur > pullDur)
                        {
                            hookProjectile.HitDone();
                            hookProjectile.transform.position = Quaternion.Euler(hookProjectile.GetPlayer().transform.eulerAngles) * hookProjectile.offset + hookProjectile.GetPlayer().transform.position;
                            hookProjectile.transform.rotation = hookProjectile.GetPlayer().transform.rotation;
                            hooked = false;

                        }

                        break;
                    case 3:
                        hookProjectile.transform.position = dude.transform.position + hookProjectile.transform.position;
                        if ((hookProjectile.GetPlayer().transform.position - dude.transform.position).magnitude < 1 || Time.time - currentDur > pullDur)
                        {
                            dude.GetComponent<BasePlayer>().rigidBody.velocity = new Vector2();
                            hookProjectile.HitDone();
                            hookProjectile.transform.position = Quaternion.Euler(hookProjectile.GetPlayer().transform.eulerAngles) * hookProjectile.offset + hookProjectile.GetPlayer().transform.position;
                            hookProjectile.transform.rotation = hookProjectile.GetPlayer().transform.rotation;
                            hooked = false;
                        }

                        break;
                }
            }
            if ((hookProjectile.transform.position - hookProjectile.GetPlayer().transform.position).magnitude > hookProjectile.getWeaponRange() && hookProjectile.isFired())
            {
                hookProjectile.Hit();
                hookProjectile.HitDone();
                hookProjectile.transform.position = Quaternion.Euler(hookProjectile.GetPlayer().transform.eulerAngles) * hookProjectile.offset + hookProjectile.GetPlayer().transform.position;

                hookProjectile.transform.rotation = hookProjectile.GetPlayer().transform.rotation;
                hooked = false;
            }
        }

        protected virtual void OnCollisionEnter2D(Collision2D other)
        {
            GameObject objectHit = other.gameObject;
            if (hookProjectile != null && !hooked)
            {
                currentDur = Time.time;
                hooked = true;
                hookProjectile.Hit();
                if (objectHit.tag == "Player")
                {
                    hookedDude = 0;
                    objectHit.GetComponent<BasePlayer>().CmdDamage(hookProjectile.getDamage());
                    dude = objectHit;
                }
                else if (objectHit.tag == "Obstacle")
                {
                    hookedDude = 1;
                }
                else
                {
                    hookedDude = -1;
                }
            }
        }
    }
}

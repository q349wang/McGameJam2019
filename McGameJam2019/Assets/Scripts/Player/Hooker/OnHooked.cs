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
                switch(hookedDude)
                {
                    case -1:
                        hookProjectile.transform.localPosition = hookProjectile.offset;
                        hooked = false;
                        break;
                    case 0:

                    break;
                    case 1:
                    break;
                }
            }
            else if (hookProjectile != null && hookProjectile.isFired())
            {
                LineRenderer lrend = GetComponent<LineRenderer>();
                if (lrend != null)
                {
                    
                }
            }
        }

        protected virtual void OnCollisionEnter2D(Collision2D other)
        {
            GameObject objectHit = other.transform.parent.gameObject;
            if (hookProjectile != null)
            {
                hooked = true;
                hookProjectile.Hit();
                if (objectHit.tag == "Player")
                {
                    hookedDude = 0;
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

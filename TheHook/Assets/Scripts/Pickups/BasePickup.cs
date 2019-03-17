using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pickups
{
    
public class BasePickup : MonoBehaviour
{
    public float cooldownSec;
    private float lastPickedUp;
    private float lastTick;
    private const float delta = 0.01f;
    private bool pickedUp;
    private SpriteRenderer m_SpriteRenderer;
    private Collider2D m_Collider2;
    // Start is called before the first frame update
    void Start()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_Collider2 = GetComponent<Collider2D>();
        m_Collider2.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastTick > delta)
        {
            transform.Rotate(new Vector3(0, 2, 0));
            lastTick = Time.time;
        }

        if (pickedUp && m_SpriteRenderer != null && Time.time - lastPickedUp > cooldownSec)
        {
            pickedUp = false;
            m_SpriteRenderer.enabled = true;
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (!pickedUp)
        {
            lastPickedUp = Time.time;
            pickedUp = true;
            if (m_SpriteRenderer != null)
            {
                m_SpriteRenderer.enabled = false;
            }

        }
    }

    float GetLastPickupTime()
    {
        return lastPickedUp;
    }
}

}
using System;
using UnityEngine;

public class PlatformerCharacter2D : MonoBehaviour
{
    [HideInInspector]
    public float m_MaxSpeed = 0f;                    // The fastest the player can travel.
    [SerializeField]
    private Transform m_Ability1;
    [SerializeField]
    private Transform m_Ability2;
    [SerializeField]
    private Transform m_Ability3;
    [SerializeField]
    private int health;
    [SerializeField]
    private int characterClass;

    private Animator m_Anim;            // Reference to the player's animator component.
    private Rigidbody2D m_Rigidbody2D;
    private int m_Mana;
    private bool isStunned = false;

    private void Awake()
    {
        // Setting up references.
        m_Ability1 = transform.Find("Ability 1");
        m_Ability2 = transform.Find("Ability 2");
        m_Ability3 = transform.Find("Ability 3");
        m_Anim = GetComponent<Animator>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Move(float moveH, float moveV)
    {
        // Move the character
        m_Rigidbody2D.velocity = new Vector2(moveH * m_MaxSpeed, moveV * m_MaxSpeed);
    }

    private void UseAbility(float a1, float a2, float a3)
    {
        if (a1 > 0)
        {
            HealerAbility1 abil = m_Ability1.GetComponent<HealerAbility1>();
            if (m_Mana >= abil.cost)
            {

            }
            //run ability 1
        }
        else if (a2 > 0)
        {
            //run ability 2
        }
        else if (a3 > 0)
        {
            //run ability 3
        }
    }
    public void FaceMouse(Vector3 pointToFace)
    {
        Vector2 direction = new Vector2(pointToFace.x - transform.position.x, pointToFace.y - transform.position.y);
        //transform.right = direction;

        float angle = Vector2.SignedAngle(Vector2.right, direction);
        m_Rigidbody2D.MoveRotation(angle);
    }
}

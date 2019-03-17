using System;
using UnityEngine;
using System.Collections;

public class PlatformerCharacter2D : MonoBehaviour
{
    [SerializeField]
    private WaitForSeconds stunDuration = new WaitForSeconds(3.0f);
    [HideInInspector]
    public float m_MaxSpeed = 0f;                    // The fastest the player can travel.

    private Animator m_Anim;            // Reference to the player's animator component.
    private Rigidbody2D m_Rigidbody2D;
    private bool isStunned;
    private bool isDead;

    public bool IsDead
    {
        get { return isDead; }
    }
    private void Awake()
    {
        // Setting up references.
        m_Anim = GetComponent<Animator>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        this.isStunned = false;
    }

    public void Move(float moveH, float moveV)
    {
        // Move the character
        if (!isStunned && !isDead)
        {
            m_Rigidbody2D.velocity = new Vector2(moveH * m_MaxSpeed, moveV * m_MaxSpeed);
        }
    }

    public void FaceMouse(Vector3 pointToFace)
    {
        Vector2 direction = new Vector2(pointToFace.x - transform.position.x, pointToFace.y - transform.position.y);
        //transform.right = direction;

        float angle = Vector2.SignedAngle(Vector2.right, direction);
        m_Rigidbody2D.MoveRotation(angle);
    }

    public IEnumerator StunPlayer()
    {
        this.isStunned = true;
        yield return stunDuration;
        this.isStunned = false;
    }

    public void Unstun()
    {
        this.isStunned = false;
    }

    public void Kill(bool condition)
    {
        this.isDead = condition;
    }

}

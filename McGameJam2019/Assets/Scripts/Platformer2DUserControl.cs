using System;
using UnityEngine;

[RequireComponent(typeof(PlatformerCharacter2D))]
public class Platformer2DUserControl : UnityEngine.Networking.NetworkBehaviour
{
    private PlatformerCharacter2D m_Character;

    private void Awake()
    {
        m_Character = GetComponent<PlatformerCharacter2D>();
    }

    void Start()
    {
        if (!isLocalPlayer)
        {
            Destroy(this);
        }
    }

    private void FixedUpdate()
    {
        // Read the inputs.
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float a1 = Input.GetAxis("Fire1");
        float a2 = Input.GetAxis("Fire2");
        float a3 = Input.GetAxis("Fire3");

        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // Pass all parameters to the character control script.
        m_Character.Move(h, v);
        m_Character.FaceMouse(mousePosition);
    }
}

using System;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        private PlatformerCharacter2D m_Character;
        
        private void Awake()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();
        }

        private void FixedUpdate()
        {
            // Read the inputs.
            bool crouch = Input.GetKey(KeyCode.LeftControl);
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            float a1 = Input.GetAxis("Fire1");
            float a2 = Input.GetAxis("Fire2");
            float a3 = Input.GetAxis("Fire3");


            // Pass all parameters to the character control script.
            m_Character.Move(h, v, crouch);
        }
    }
}

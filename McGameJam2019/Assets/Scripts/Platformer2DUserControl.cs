using System;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        private PlatformerCharacter2D m_Character;
        private bool m_Jump;


        private void Awake()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();
        }


        private void Update()
        {
            if (!m_Jump)
            {
                // Read the jump input in Update so button presses aren't missed.
                m_Jump = UnityEngine.Input.GetButtonDown("Jump");
            }
        }


        private void FixedUpdate()
        {
            // Read the inputs.
            bool crouch = UnityEngine.Input.GetKey(KeyCode.LeftControl);
            float h = UnityEngine.Input.GetAxis("Horizontal");
            float v = UnityEngine.Input.GetAxis("Vertical");
            float a1 = UnityEngine.Input.GetAxis("Fire1");
            float a2 = UnityEngine.Input.GetAxis("Fire2");
            float a3 = UnityEngine.Input.GetAxis("Fire3");


            // Pass all parameters to the character control script.
            m_Character.Move(h, v, crouch, m_Jump);
            m_Jump = false;
        }
    }
}

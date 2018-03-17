using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.Networking;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class ThirdPersonUserControl : NetworkBehaviour
    {
        private ThirdPersonCharacter m_Character; 	// A reference to the ThirdPersonCharacter on the object
        private Transform m_Cam;                  	// A reference to the main camera in the scenes transform
        private Vector3 m_CamForward;             	// The current forward direction of the camera
        private Vector3 m_Move;                   	// the world-relative desired move direction, calculated from the camForward and user input.
		private bool m_Dodge;
		private bool m_Attack;
		private bool m_AnswerTexto;					// Answer a texto

        
        private void Start()
        {
            // get the transform of the main camera
            if (Camera.main != null)
            {
                m_Cam = Camera.main.transform;
            }
            else
            {
                Debug.LogWarning(
                    "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", gameObject);
                // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
            }

            // get the third person character ( this should never be null due to require component )
            m_Character = GetComponent<ThirdPersonCharacter>();
        }


        private void Update()
        {
            if (!isServer)
                return;

			if (!m_Dodge)
				m_Dodge = CrossPlatformInputManager.GetButtonDown("Dodge");

			if (!m_Attack)
				m_Attack = CrossPlatformInputManager.GetButtonDown ("Attack");

			if (!m_AnswerTexto)
				m_AnswerTexto = CrossPlatformInputManager.GetButtonDown ("AnswerTexto");
        }


        // Fixed update is called in sync with physics
        private void FixedUpdate()
        {
            if (!isServer)
                return;

            // read inputs
            float h = CrossPlatformInputManager.GetAxis ("Horizontal1");
			float v = CrossPlatformInputManager.GetAxis ("Vertical1");

			// calculate move direction to pass to character
			if (m_Cam != null) {
				// calculate camera relative direction to move:
				m_CamForward = Vector3.Scale (m_Cam.forward, new Vector3 (1, 0, 1)).normalized;
				m_Move = v * m_CamForward + h * m_Cam.right;
			} else {
				// we use world-relative directions in the case of no main camera
				m_Move = v * Vector3.forward + h * Vector3.right;
			}
			// pass all parameters to the character control script
			m_Character.CmdMove(m_Move, m_Dodge, m_Attack);

			// calls function for actions that don't requiere animation
			if (m_AnswerTexto)
			{
				Debug.Log ("Answering Texto!");
				// TODO
				// m_Character.AnswerTexto ();
			}
				
			// Reset Triggers
			m_Dodge = false;
			m_Attack = false;
			m_AnswerTexto = false;
        }
    }
}

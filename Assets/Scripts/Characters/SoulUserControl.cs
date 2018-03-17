using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.Networking;

namespace UnityStandardAssets.Characters.Soul
{
	[RequireComponent(typeof (SoulCharacter))]
	public class SoulUserControl : NetworkBehaviour {

		private SoulCharacter m_Character;	// A reference to the Soul on the object
		private Transform m_Cam;			// A reference to the main camera in the scenes transform
		private Vector3 m_CamForward;       // The current forward direction of the camera
		private Vector3 m_Move;             // the world-relative desired move direction, calculated from the camForward and user input.
		private Vector3 m_Orient;
		private bool m_Dodge;
		private bool m_Shoot;

		// Use this for initialization
		private void Start ()
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

			m_Character = GetComponent<SoulCharacter> ();
		}

		// Update is called once per frame
		private void Update () {
            if (isServer)
                return;

			if (!m_Dodge)
				m_Dodge = CrossPlatformInputManager.GetButtonDown("Dodge");

			if (!m_Shoot)
				m_Shoot = CrossPlatformInputManager.GetButton("Shoot");
		}

		private void FixedUpdate()
        {
            if (isServer)
                return;

            // read inputs
            // for position (left joystick)
            float h1 = CrossPlatformInputManager.GetAxis("Horizontal1");
			float v1 = CrossPlatformInputManager.GetAxis("Vertical1");

			// for orientation (right joystick)
			float h2 = CrossPlatformInputManager.GetAxis("Horizontal2");
			float v2 = CrossPlatformInputManager.GetAxis("Vertical2");

			// calculate move direction and orientation to pass to character
			if (m_Cam != null)
			{
				m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;

				// calculate camera relative direction to move:
				m_Move = v1*m_CamForward + h1*m_Cam.right;

				// calculate camera relative orientation for rotation:
				if(h2 != 0f || v2 != 0f)
				{
					m_Orient = v2*m_CamForward + h2*m_Cam.right;
				}
				else if(h1 != 0f || v1 != 0f)
				{
					m_Orient = m_Move;
				}
			}
			else
			{
				// we use world-relative directions in the case of no main camera
				m_Move = v1*Vector3.forward + h1*Vector3.right;
			}

			m_Character.Move (m_Move, m_Orient, m_Dodge, m_Shoot);

			// Reset Triggers
			m_Dodge = false;
			m_Shoot = false;
		}
	}
}


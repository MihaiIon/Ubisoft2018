using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Characters.Soul
{
	[RequireComponent(typeof(Rigidbody))]
	[RequireComponent(typeof(CapsuleCollider))]
	public class SoulCharacter : MonoBehaviour {

		[SerializeField] float speedMultiplier = 6f;
		[SerializeField] float decelerationMultiplier = 2f;
		[SerializeField] float dodgeMultiplier = 8f;

		Rigidbody m_Rigidbody;
		CapsuleCollider m_Capsule;
		Vector3 m_Move;
		float moveMultiplier;

		// Use this for initialization
		void Start () {

			m_Rigidbody = GetComponent<Rigidbody> ();
			m_Capsule = GetComponent<CapsuleCollider> ();

			moveMultiplier = speedMultiplier;

			m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
		}
		
		// Called by the User Control Script
		public void Move (Vector3 move, Vector3 orient, bool dodge, bool shoot) {

			// Move the character
			// If the magnitude of the movement is too small
			// We decelerate gradually to avoid abrupt stop
			if (Vector3.Magnitude (move) > 0.2)
				m_Move = move;
			else
				m_Move = Vector3.Lerp (m_Move, Vector3.zero, Time.deltaTime * decelerationMultiplier);

			// Dodging increases the moving speed for a short time
			if (dodge)
				moveMultiplier = speedMultiplier * dodgeMultiplier;

			// Decelerate moving speed to regular speed gradually
			moveMultiplier = Mathf.Lerp (moveMultiplier,  speedMultiplier, Time.deltaTime * 5f);

			// Update player position
			transform.position = transform.position + m_Move * Time.deltaTime * moveMultiplier;

			// Orient the character
			float m_Angle = Mathf.Atan2 (orient.x, orient.z);
			Quaternion target = Quaternion.Euler (0, Mathf.Rad2Deg * m_Angle, 0);
			transform.rotation = Quaternion.Lerp(transform.rotation, target, Time.deltaTime * 10f);

			// Shoot beam of light
			if (shoot)
				Shoot (orient);
		}

		// Shoot beam of light
		private void Shoot (Vector3 orient){
			// TODO
			bool hit = Physics.Raycast (transform.position, orient, 10f);
			Debug.Log ("Shooting! ");
			Debug.Log (hit);

			Debug.DrawRay (transform.position, orient * 100, Color.red, Time.deltaTime);
		}
	}
}

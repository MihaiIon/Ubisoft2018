using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Characters.Soul
{
	[RequireComponent(typeof(Rigidbody))]
	[RequireComponent(typeof(CapsuleCollider))]

	public class SoulCharacter : MonoBehaviour {


		[Header("Player Stats")]
		[SerializeField] float shootRange = 20f;
		[SerializeField] float shootDamage = 1f;

		[Header("Movement")]
		[SerializeField] float speedMultiplier = 6f;
		[SerializeField] float decelerationMultiplier = 2f;
		[SerializeField] float dodgeMultiplier = 8f;

		[Header("Light")]
		public Light m_SpotLight;
		[SerializeField] float shootingAngle = 60f;

		Rigidbody m_Rigidbody;
		CapsuleCollider m_Capsule;
		Vector3 m_Move;

		float moveMultiplier;
		private float lightMaxAngle;
		private float lightAngle;

		// Use this for initialization
		void Start () {

			m_Rigidbody = GetComponent<Rigidbody> ();
			m_Capsule = GetComponent<CapsuleCollider> ();

			moveMultiplier = speedMultiplier;

			// Initialize the angle of the spotlight as the maximum angle
			lightMaxAngle = m_SpotLight.spotAngle;
			lightAngle = lightMaxAngle;

			m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
		}

		void Update()
		{
			
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

			// Change the angle of the spotlight
			m_SpotLight.spotAngle = shootingAngle;

			// Cast a ray in front of the boy
			Ray ray = new Ray(transform.position - Vector3.up * 0.5f, transform.forward);
			RaycastHit hit;
			Debug.DrawRay(ray.origin, ray.direction * shootRange, Color.red, Time.deltaTime);
			bool isThereAhit = Physics.Raycast(ray, out hit, shootRange);

			// If we hit something
			if(isThereAhit){

				// If what we hit is an enemy
				if (hit.transform.gameObject.layer == 9)
				{
					// Warns the enemy it got hit
					Debug.Log ("Hit Enemy!");
					hit.transform.gameObject.GetComponent<EnemyCharacter>().Shot(shootDamage);
				}
				else{
					Debug.Log ("Hit Not Enemy!");
				}
			}
			else
			{
				Debug.Log ("Hit Nothing!");
			}
		}
	}
}

using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
	[RequireComponent(typeof(Rigidbody))]
	[RequireComponent(typeof(CapsuleCollider))]
	[RequireComponent(typeof(Animator))]
	public class ThirdPersonCharacter : MonoBehaviour
	{
		[SerializeField] float m_MovingTurnSpeed = 360;
		[SerializeField] float m_StationaryTurnSpeed = 180;
		[SerializeField] float m_RunCycleLegOffset = 0.2f; //specific to the character in sample assets, will need to be modified to work with others
		[SerializeField] float m_MoveSpeedMultiplier = 1f;
		[SerializeField] float m_DodgeSpeedMultiplier = 1f;
		[SerializeField] float m_AnimSpeedMultiplier = 1f;

		public float length = 2f;

		Rigidbody m_Rigidbody;
		Animator m_Animator;
		float m_TurnAmount;
		float m_ForwardAmount;
		Vector3 m_move;
		Vector3 m_GroundNormal;
		float m_CapsuleHeight;
		Vector3 m_CapsuleCenter;
		CapsuleCollider m_Capsule;
		bool m_Dodge;
		bool m_Attack;
		bool m_GetHit;


		void Start()
		{
			m_Animator = GetComponent<Animator>();
			m_Rigidbody = GetComponent<Rigidbody>();
			m_Capsule = GetComponent<CapsuleCollider>();
			m_CapsuleHeight = m_Capsule.height;
			m_CapsuleCenter = m_Capsule.center;

			m_Rigidbody.constraints =	RigidbodyConstraints.FreezeRotationX |
										RigidbodyConstraints.FreezeRotationY |
										RigidbodyConstraints.FreezeRotationZ |
										RigidbodyConstraints.FreezePositionY;
		}


		public void Move(Vector3 move, bool dodge, bool attack)
		{
			// convert the world relative moveInput vector into a local-relative
			// turn amount and forward amount required to head in the desired
			// direction.
			if (move.magnitude > 1f) move.Normalize();
			m_move = move;
			move = transform.InverseTransformDirection(move);
			move = Vector3.ProjectOnPlane(move, m_GroundNormal);
			m_TurnAmount = Mathf.Atan2(move.x, move.z);
			m_ForwardAmount = move.z;

			ApplyExtraTurnRotation();

			// control and velocity handling is different when grounded and airborne:
			if(dodge)
				m_Dodge = true;

			if(attack && m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Movement"))
				m_Attack = true;

			//Adjust Capsule Collider Height and Center in function of the animator paramater
			m_Capsule.height = m_CapsuleHeight * m_Animator.GetFloat ("ColliderHeight");
			m_Capsule.center = m_CapsuleCenter * m_Animator.GetFloat ("ColliderHeight");

			// send input and other state parameters to the animator
			Vector3 tmp = transform.position;

			UpdateAnimator (move);				
		}


		void UpdateAnimator(Vector3 move)
		{
			// update the animator parameters
			// Movement parameters
			m_Animator.SetFloat("Forward", m_ForwardAmount, 0.1f, Time.deltaTime);
			m_Animator.SetFloat("Turn", m_TurnAmount, 0.1f, Time.deltaTime);

			// Get Hit
			if (m_GetHit)
				m_Animator.SetTrigger ("GetHit");

			// Dodge
			if(m_Dodge){
				m_Animator.SetTrigger ("Dodge");
				m_Dodge = false;
			}
			// Attack
			else if(m_Attack){
				m_Animator.SetTrigger ("Attack");
				m_Attack = false;
			}

			// the anim speed multiplier allows the overall speed of walking/running to be tweaked in the inspector,
			// which affects the movement speed because of the root motion.
			m_Animator.speed = m_AnimSpeedMultiplier;
		}

		void ApplyExtraTurnRotation()
		{
			// help the character turn faster (this is in addition to root rotation in the animation)
			float turnSpeed = Mathf.Lerp(m_StationaryTurnSpeed, m_MovingTurnSpeed, m_ForwardAmount);
			transform.Rotate(0, m_TurnAmount * turnSpeed * Time.deltaTime, 0);
		}


		public void OnAnimatorMove()
		{
			// we implement this function to override the default root motion.
			// this allows us to modify the positional speed before it's applied.
			if (Time.deltaTime > 0)
			{
				// Choose between Speed Modifier use, regular movement or dodge. Allows to control each independently.
				float speedMultiplier = (m_Animator.GetCurrentAnimatorStateInfo (0).IsName ("Dodge")) ? m_DodgeSpeedMultiplier : m_MoveSpeedMultiplier;

				// Get the normal of the ground in front
				Vector3 groundNormal = CheckGround ();

				// Prevents the player from going uphill
				Vector3 v = Vector3.ProjectOnPlane (m_Animator.deltaPosition, groundNormal);
				v.y = 0;
				v = (v * speedMultiplier) / Time.deltaTime;

				// we preserve the existing y part of the current velocity.
				v.y = m_Rigidbody.velocity.y;
				m_Rigidbody.velocity = v;
			}
		}

		// Returns the normal of the ground in front of the player
		private Vector3 CheckGround(){

			// Orientation the player is facing
			Vector3 direction = Quaternion.Euler (transform.rotation.eulerAngles) * Vector3.forward;
			direction = direction * length + Vector3.down * 0.5f;

			RaycastHit hit;
			Physics.Raycast (transform.position + Vector3.up * 0.3f, direction, out hit, direction.magnitude);
			Debug.DrawRay (transform.position + Vector3.up * 0.3f, direction, Color.red, Time.deltaTime);

			return hit.normal;
		}
	}
}

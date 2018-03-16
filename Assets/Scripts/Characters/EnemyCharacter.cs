using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace UnityStandardAssets.Characters
{
	[RequireComponent(typeof(Rigidbody))]
	[RequireComponent(typeof(CapsuleCollider))]
	[RequireComponent(typeof(Animator))]
	[RequireComponent(typeof(MaterialEmission))]
	[RequireComponent(typeof(NavMeshAgent))]

	public class EnemyCharacter : MonoBehaviour {

		[Header("Enemy Stats")]
		[SerializeField] float life = 100;
		[SerializeField] float attackRange = 2f;
		[SerializeField] int attackDamage = 20;

		[Header("Movement")]
		[SerializeField] float m_MovingTurnSpeed = 360;
		[SerializeField] float m_StationaryTurnSpeed = 180;
		[SerializeField] float m_RunCycleLegOffset = 0.2f; //specific to the character in sample assets, will need to be modified to work with others
		[SerializeField] float m_MoveSpeedMultiplier = 1f;
		[SerializeField] float m_AnimSpeedMultiplier = 1f;

		Rigidbody m_Rigidbody;
		Animator m_Animator;
		CapsuleCollider m_Capsule;
		MaterialEmission m_MaterialEmission;
		NavMeshAgent agent;

		float m_TurnAmount;
		float m_ForwardAmount;
		Vector3 m_move;
		Vector3 m_GroundNormal;
		float m_CapsuleHeight;
		Vector3 m_CapsuleCenter;
		bool m_Stun;
		bool m_Attack;
		bool m_Die;


		void Start()
		{
			m_Animator = GetComponent<Animator>();
			m_Rigidbody = GetComponent<Rigidbody>();
			m_Capsule = GetComponent<CapsuleCollider>();
			m_MaterialEmission = GetComponent<MaterialEmission> ();
			agent = GetComponent<NavMeshAgent> ();
			m_CapsuleHeight = m_Capsule.height;
			m_CapsuleCenter = m_Capsule.center;

			m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

			// Prevents the agent from updtating the position automatically
			agent.updatePosition = false;
			agent.updateRotation = false;
		}


		// Used to manage when 
		public void GetHit ()
		{

			if (m_Stun){
				Die ();
			}
		}

		public void Shot(float damage)
		{
			life -= damage;
		}

		public void GetStunned()
		{
			m_Stun = true;
		}

		void Die()
		{

			m_Die = true;
			m_Rigidbody.isKinematic = true;
			// TODO enemy disapear
		}

		// Funtion that launches an attack
		// Called by an event in the Animator to match the animation
		void Attack (){

			// Cast a ray in front of the enemy
			Ray ray = new Ray(transform.position + Vector3.up * 0.6f, transform.forward);
			RaycastHit hit;
			Debug.DrawRay(ray.origin, ray.direction * attackRange, Color.red, 1f);
			bool isThereAhit = Physics.Raycast(ray, out hit, attackRange);

			// If we hit something
			if(isThereAhit){

				// If what we hit is an enemy
				if (hit.transform.gameObject.layer == 8)
				{
					// Warns the enemy it got hit
					Debug.Log ("Hit Boy!");
					hit.transform.gameObject.GetComponent<ThirdPerson.ThirdPersonCharacter>().GetHit(attackDamage);
				}
				else{
					Debug.Log ("Hit Nothing!");
				}
			}
			else
			{
				Debug.Log ("Hit Nothing!");
			}
		}

		public void Move(Vector3 move, bool attack)
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

			// An enemy can't attack while he's stunned
			if(attack && m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Movement"))
			{
				m_Attack = true;
			}

			ApplyExtraTurnRotation();

			//Adjust Capsule Collider Height and Center in function of the animator paramater
			//m_Capsule.height = m_CapsuleHeight * m_Animator.GetFloat ("ColliderHeight");
			//m_Capsule.center = m_CapsuleCenter * m_Animator.GetFloat ("ColliderHeight");

			// send input and other state parameters to the animator
			UpdateAnimator(move);
		}

		void UpdateAnimator(Vector3 move)
		{
			// update the animator parameters
			// Movement parameters
			m_Animator.SetFloat("Forward", m_ForwardAmount, 0.1f, Time.deltaTime);
			m_Animator.SetFloat("Turn", m_TurnAmount, 0.1f, Time.deltaTime);
			m_Animator.SetBool("Stun", m_Stun);
			m_Animator.SetBool ("Die", m_Die);

			// Attack
			if(m_Attack){
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
				float speedMultiplier = m_MoveSpeedMultiplier;
				Vector3 v = (m_Animator.deltaPosition * speedMultiplier) / Time.deltaTime;

				// we preserve the existing y part of the current velocity.
				v.y = m_Rigidbody.velocity.y;
				m_Rigidbody.velocity = v;
			}
		}
	}
}





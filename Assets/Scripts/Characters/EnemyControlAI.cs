using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace UnityStandardAssets.Characters
{

	[RequireComponent(typeof(NavMeshAgent))]
	[RequireComponent(typeof(EnemyCharacter))]
	[RequireComponent(typeof(MaterialEmission))]

	public class EnemyControlAI : MonoBehaviour {

		// State the enemy can be in :
		// ATTACKING : enemy is moving toward the target and attack if the target is in reach.
		// STUNNED : enemy doesn't move and is vulnarable to the boy's attack. Regains life overtime.
		// STANDBY : enemy just stands there if the target is not in a certain radius.
		// DEFEND : TODO Enemy not moving but attacks if target in reach. Purpose of blocking a passage.
		// DEAD : TODO Enemy is dead.
		public enum State{ATTACKING, STUNNED, STANDBY}

		public State state;
		public Transform target;

		NavMeshAgent agent;
		EnemyCharacter m_EnemyCharacter;
		MaterialEmission m_MaterialEmission;


		// Use this for initialization
		void Start () {

			// Get required components
			m_EnemyCharacter = GetComponent<EnemyCharacter> ();
			m_MaterialEmission = GetComponent<MaterialEmission> ();
			agent = GetComponent<NavMeshAgent>();

			// Initialize state
			state = State.STANDBY;
		}
		
		// Update is called once per frame
		void Update ()
		{
			// Sets the material emission if the enemy was shot or if he is stunned
			if (m_EnemyCharacter.isShot || state == State.STUNNED)
				m_MaterialEmission.emit = true;
			else
				m_MaterialEmission.emit = false;
				
			// Checks conditions that would make the state change
			// and change state if needed
			CheckState ();

			m_EnemyCharacter.isShot = false;
		}

		// Execute everything once
		void FixedUpdate ()
		{
			switch (state)
			{
			case State.ATTACKING:
				
				agent.destination = target.position;
				Vector3 worldDeltaPosition = agent.nextPosition - transform.position;

				bool attack = m_EnemyCharacter.CheckIfTargetInReach ();

				m_EnemyCharacter.Move (worldDeltaPosition, attack);
				break;

			case State.STUNNED:
				m_EnemyCharacter.RegainLife ();
				m_EnemyCharacter.Move (Vector3.zero, false);
				break;

			case State.STANDBY:
				m_EnemyCharacter.Move (Vector3.zero, false);
				break;
			}
		}


		// Checks the conditions for switching state given the current state, 
		void CheckState ()
		{
			// In Any state
			if(m_EnemyCharacter.life <= 0 && state != State.STUNNED)
			{
				SetStunned ();
				return;
			}

			float targetDistance = Vector3.Magnitude (target.position - transform.position);

			// Given we are in a specific state
			switch(state)
			{
			// Case the state is currently ATTACKING
			case State.ATTACKING:
				
				if (targetDistance > m_EnemyCharacter.distanceThreshold) {
					SetStandby ();
				}
				break;
			
				// Case the state is currently STUNNED
			case State.STUNNED:
				
				if (m_EnemyCharacter.life == m_EnemyCharacter.maxLife)
					SetStandby ();
				break;

				// Case the state is currently STANDBY
			case State.STANDBY:
				
				if (targetDistance < m_EnemyCharacter.distanceThreshold) {
					SetAttacking ();
				}
				break;
			}
		}


		// Setters for the State of the Enemy
		void SetStunned ()
		{
			state = State.STUNNED;
			m_MaterialEmission.BoostEmission ();
			m_MaterialEmission.SwitchColor (2);
			agent.isStopped = true;
		}

		void SetAttacking ()
		{
			m_MaterialEmission.SwitchColor (1);
			state = State.ATTACKING;
			agent.isStopped = false;
		}

		void SetStandby ()
		{
			m_MaterialEmission.SwitchColor (1);
			state = State.STANDBY;
			agent.isStopped = true;
		}
	}
}

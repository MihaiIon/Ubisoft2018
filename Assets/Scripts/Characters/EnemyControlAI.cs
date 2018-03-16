using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace UnityStandardAssets.Characters
{

	[RequireComponent(typeof(NavMeshAgent))]
	[RequireComponent(typeof(EnemyCharacter))]

	public class EnemyControlAI : MonoBehaviour {

		// State the enemy can be in
		public enum State{ATTACKING, STUNNED, STANDBY}

		[Header("Enemy Stats")]
		[SerializeField] float stunTime = 5f;
		[SerializeField] float distanceThreshold = 10f;

		public State state;
		public Transform target;

		NavMeshAgent agent;
		EnemyCharacter enemyCharacter;

		bool isStunned;

		// Use this for initialization
		void Start () {

			// Get required components
			enemyCharacter = GetComponent<EnemyCharacter> ();
			agent = GetComponent<NavMeshAgent>();

			// Initialize
			state = State.STANDBY;
			isStunned = false;
		}
		
		// Update is called once per frame
		void Update ()
		{
			Debug.Log (Vector3.Magnitude (target.position - transform.position));
			if(Vector3.Magnitude (target.position - transform.position) > distanceThreshold)
			{
				SetStandby ();
			}
			else if(state != State.ATTACKING)
			{
				SetAttacking ();
			}


		}

		void FixedUpdate ()
		{
			switch (state)
			{
			case State.ATTACKING:
				agent.destination = target.position;
				Vector3 worldDeltaPosition = agent.nextPosition - transform.position;
				enemyCharacter.Move (worldDeltaPosition, true);
				break;

			case State.STUNNED:
				enemyCharacter.Move (Vector3.zero, false);
				break;

			case State.STANDBY:
				enemyCharacter.Move (Vector3.zero, false);
				break;
			}
		}

		// Setters for the State of the Enemy

		void SetStunned ()
		{
			state = State.STUNNED;
			agent.isStopped = true;
		}

		void SetAttacking ()
		{
			state = State.ATTACKING;
			agent.isStopped = false;
		}

		void SetStandby ()
		{
			state = State.STANDBY;
			agent.isStopped = true;
		}
	}
}

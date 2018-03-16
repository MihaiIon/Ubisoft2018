using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]

public class Pathfind : MonoBehaviour {
    
    private UnityEngine.AI.NavMeshAgent agent;

    public Transform goal;

    // Use this for initialization
    void Start () {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		agent.updatePosition = false;
		agent.updateRotation = false;

	}
	
	// Update is called once per frame
	void Update () {
        agent.destination = goal.position;
	}

	public Vector3 GetNextPosition(){

		return agent.nextPosition;
	}

    public void SetGoal (Transform goal)
    {
        this.goal = goal;
    }
}

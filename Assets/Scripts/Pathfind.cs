using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfind : MonoBehaviour {
    
    private UnityEngine.AI.NavMeshAgent agent;

    public Transform goal;

    // Use this for initialization
    void Start () {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        agent.destination = goal.position;
	}

    public void SetGoal (Transform goal)
    {
        this.goal = goal;
    }
}

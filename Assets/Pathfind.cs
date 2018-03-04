using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfind : MonoBehaviour {
    
    private UnityEngine.AI.NavMeshAgent agent;
    private GameObject goal;

    // Use this for initialization
    void Start () {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        agent.destination = goal.transform.position;
	}

    public void SetGoal (GameObject goal)
    {
        this.goal = goal;
    }
}

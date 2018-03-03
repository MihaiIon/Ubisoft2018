using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfind : MonoBehaviour {

    // private GameObjet go;

    public Transform goal;
    private UnityEngine.AI.NavMeshAgent agent;

    // Use this for initialization
    void Start () {

        // this.goal = goal;
        // agent = go.AddComponent<UnityEngine.AI.NavMeshAgent>();

        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.destination = goal.position;
	}
	
	// Update is called once per frame
	void Update () {
        agent.destination = goal.position;
	}
}

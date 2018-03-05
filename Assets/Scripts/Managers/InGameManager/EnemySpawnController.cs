using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour {

    // GameManager reference
    private InGameManager inGameManager;
    public EnemySpawnManager enemySpawnManager;

    // Use this for initialization
    void Start () {
        inGameManager = GetComponent<InGameManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

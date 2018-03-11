using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour {

    // Manager reference
    private GameManager gameManager;
    public EnemySpawnManager enemySpawnManager;

    // Use this for initialization
    void Start () {

        gameManager = GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void IncreaseDifficulty ()
    {
        enemySpawnManager.IncreaseDifficulty();
    }

    public void LowerDifficulty ()
    {
        enemySpawnManager.LowerDifficulty();
    }

    public GameObject[] SpawnEnemies (Transform player)
    {
        return enemySpawnManager.SpawnEnemies(player);
    }
}

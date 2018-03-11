using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameController : MonoBehaviour {

    // Manager references
    private GameManager gameManager;
    public InGameManager inGameManager;

    // Controller references
    private EnemySpawnController enemySpawnController;

    // Variables
    private GameObject player;
    private GameObject[] shadows;

    private void Awake()
    {
        gameManager = GetComponent<GameManager>();
        enemySpawnController = GetComponent<EnemySpawnController>();
    }

    /// <summary>
    /// 
    /// </summary>
	public void Init ()
    {
        player = inGameManager.Init();
        shadows = enemySpawnController.SpawnEnemies(player.transform);
    }


    /// <summary>
    /// 
    /// </summary>
    public void Dismiss()
    {

    }
}

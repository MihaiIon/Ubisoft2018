using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour {
    
    public GameObject shadowPrefab;

    public enum Difficulty
    {
        VERY_EASY = 1,
        EASY,
        NORMAL,
        HARD,
        VERY_HARD,
        VETERAN,
        GOD
    };

    [SerializeField] public Difficulty difficultyLevel;
    [SerializeField] public float minRadiusDecay = 0.75F;
    [SerializeField] public float maxRadiusDecay = 0.75F;
    [SerializeField] public int minRadius = 8;
    [SerializeField] public int maxRadius = 14;

    // Use this for initialization
    void Start () {
        // currentDif = Difficulty.NORMAL;
    }
	
	// Update is called once per frame
	void Update () {

    }

    public void IncreaseDifficulty()
    {
        if (difficultyLevel < Difficulty.GOD)
            difficultyLevel++;
    }

    public void LowerDifficulty()
    {
        if (difficultyLevel > Difficulty.VERY_EASY)
            difficultyLevel--;
    }

    public GameObject[] SpawnEnemies(Transform player)
    {
        int difficultyValue = (int)difficultyLevel;

        int enemyCount = Mathf.FloorToInt(difficultyValue / 2) + 3;
        float minSpawnRadius = minRadius - difficultyValue * minRadiusDecay;
        float maxSpawnRadius = maxRadius - difficultyValue * maxRadiusDecay;
        float spawnDirection = Random.Range(0, 2 * Mathf.PI);
        float spawnSpread = (difficultyValue / (int)Difficulty.GOD) * Mathf.PI;

        GameObject[] shadows = new GameObject[enemyCount];

        for (int i = 0; i < enemyCount; i++)
        {
            float radius = Random.Range(minSpawnRadius, maxSpawnRadius);
            float angle = Random.Range(spawnDirection - spawnSpread, spawnDirection + spawnSpread);

            float x = player.transform.position.x + radius * Mathf.Cos(angle);
            float z = player.transform.position.z + radius * Mathf.Sin(angle);

            shadows[i] = Instantiate(shadowPrefab, new Vector3(x, 1, z), Quaternion.identity) as GameObject;
            shadows[i].GetComponent<Pathfind>().SetGoal(player.transform);
        }

        return shadows;
    }
}

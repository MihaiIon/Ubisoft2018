using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour {

    public GameObject playerPrefab;
    public GameObject shadowPrefab;
    public int shadowCount;

    private GameObject player;
    private GameObject[] shadows;

    private void Start()
    {
        player = Instantiate(playerPrefab, new Vector3(0, 1, 0), Quaternion.identity) as GameObject;

        shadows = new GameObject[shadowCount];
        for (int i = 0; i < shadowCount; i++)
        {
            float radius = Random.Range(8, 15);
            float angle = Random.Range(0, 2 * Mathf.PI);

            float x = player.transform.position.x + radius * Mathf.Cos(angle);
            float z = player.transform.position.z + radius * Mathf.Sin(angle);

            shadows[i] = Instantiate(shadowPrefab, new Vector3(x, 1, z), Quaternion.identity) as GameObject;
            shadows[i].GetComponent<Pathfind>().SetGoal(player.transform);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

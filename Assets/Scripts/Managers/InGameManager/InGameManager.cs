using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour {

    public GameObject playerPrefab;
    public GameObject shadowPrefab;
    public int shadowCount;

    private GameObject player;
    private GameObject[] shadows;

    // Use this for initialization
    void Start ()
    {
        player = Instantiate(playerPrefab, new Vector3(-20, 1, -20), Quaternion.identity) as GameObject;

        shadowCount = 5;
        shadows = new GameObject[shadowCount];

        for (int i = 0; i < shadowCount; i++)
        {
            shadows.SetValue(Instantiate(shadowPrefab, new Vector3(5 * i, 1, 5 * i), Quaternion.identity) as GameObject, i);
            shadows[i].AddComponent<Pathfind>();
            shadows[i].GetComponent<Pathfind>().SetGoal(player);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

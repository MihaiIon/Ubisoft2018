using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour {

    public GameObject playerPrefab;

    private GameObject player;

    void Start()
    {

    }

    public GameObject Init ()
    {
        player = Instantiate(playerPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        return player;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

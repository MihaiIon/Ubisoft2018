using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour {

    public GameObject player;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position =  player.transform.position+Vector3.up + 3*Vector3.back;
        transform.LookAt(player.transform.forward);
    }
}

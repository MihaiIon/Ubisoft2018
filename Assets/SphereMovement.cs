using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereMovement : MonoBehaviour {
    private Rigidbody rb;
    private Vector3 vitesse;
    public float x, z;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        vitesse = new Vector3(x, 0, z);
	}
	
	// Update is called once per frame
	void Update () {

	}

    private void FixedUpdate()
    {
        rb.AddForce(vitesse);
    }
}

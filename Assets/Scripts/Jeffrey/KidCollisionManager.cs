using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidCollisionManager : MonoBehaviour {

	/*
    PlayerHealthManager phm;

	// Use this for initialization
	void Start () {
        print("collecting");
        phm = GetComponent<PlayerHealthManager>();
	}

    private void Update()
    {
        print(GetComponent<PlayerHealthManager>().getHealth());
    }

    //Verifies collision trigger
    private void OnCollisionEnter(Collision col)
    {
        if(col.transform.gameObject.layer == 10) //10 is lightFragment layer        {
        {
            //collect(other)
            Object.Destroy(col.transform.gameObject);
            phm.addHealth(5);
        }
        else if(col.transform.gameObject.layer == 9)
        {
            //Do knockBack + reduce health
            phm.removeHealth(5);
            GetComponent<Rigidbody>().velocity = -(col.transform.position - transform.position).normalized * 10;
        }

    }
    */
}

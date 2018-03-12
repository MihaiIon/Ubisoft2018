using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drop : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        Launch();
    }

    void Launch()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * 0.5f, ForceMode.Impulse);
    }

}

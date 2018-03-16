using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charMovement : MonoBehaviour
{

    [SerializeField]float speed;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (Input.GetAxis("Horizontal") < 0)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        if (Input.GetAxis("Vertical") > 0)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        if (Input.GetAxis("Vertical") < 0)
        {
            transform.Translate(Vector3.back * speed * Time.deltaTime);
        }        
    }
}
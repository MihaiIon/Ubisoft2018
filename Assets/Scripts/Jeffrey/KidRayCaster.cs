using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidRayCaster : MonoBehaviour {

    [SerializeField] float rayLength = 5.0f;

	// Update is called once per frame
	void Update () {
        //Activate RayCast with input
        Ray ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * rayLength, Color.black);
        RaycastHit hit;
        bool isThereAhit = Physics.Raycast(ray, out hit, rayLength);
        if (Input.GetButton("Fire2"))
        {
            //Generates a ray that starts from center of kid
            Debug.DrawRay(ray.origin, ray.direction * rayLength, Color.red);
            if (isThereAhit)
            {
                switch (hit.transform.gameObject.layer)
                { 
                    case 8: //DogItems Layer
                        print("DogItems");
                        //TODO: a kill all to eliminate all enemies in area that would follow.
                        //Master.killAll();
                        //TODO: initiate grabing animation + remove item from world + add to inventory
                        /*
                          Animation.Play("Grab"); 
                         */
                        break;
                    case 9: //Enemy Layer
                        print("Enemy");
                        hit.transform.gameObject.GetComponent<Enemy_AI>().kill();
                        //TODO: Verify if enemy is stunned, if true, kill enemy.
                        /*if(hit.isStunned == true)
                        {
                            Animation.Play("Kick"); 
                            hit.kill()
                        }
                        else
                        {
                          ///Nothing here? Maybe a knockBack?
                        }
                         */
                        break;
                    default:
                        print("Nothing here");
                        break;
                }
            }
        }
        
	}
}

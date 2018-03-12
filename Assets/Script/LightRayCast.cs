using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRayCast : MonoBehaviour
{
    [SerializeField] float knockBackForce = 10;

    //EveryThing related to enemy detection is in FieldOfView.cs
    // Update is called once per frame
    void Update()
    {
        //Activate RayCast with input
        if (Input.GetButton("Fire1"))
        {
            List<Transform> Targets = GetComponent<FieldOfView>().visibleTargets;
            //Va voir la liste de hits du fieldOfView
            //Passes through list of hits
            foreach (Transform target in Targets)
            {
                Debug.DrawRay(transform.position, ((target.position - transform.position).normalized) * (Vector3.Distance(transform.position, target.position)), Color.yellow);
                switch (target.transform.gameObject.layer)
                {
                    case 8: //DogItems Layer
                        print("DogItems");
                        //Maybe do a High Light
                        break;
                    case 9: //Enemy Layer
                        print("Enemy");
                        //TODO: stun enemy
                        target.transform.gameObject.GetComponent<Enemy_AI>().ModifySpeed(1/(Vector3.Distance(target.transform.position, transform.position)) *0.05f);
                        break;
                    default:
                        print("Nothing here");
                        break;
                }
            }
        }
        else if (Input.GetButtonDown("Fire3"))
        {
            print("EXPLOSION");
            Explosion(GetComponent<FieldOfView>().targetsInViewRadius);            
        }
    }
    /// <summary>
    /// Explosion knockbacks enemy but doesn't reduce speed
    /// </summary>
    /// <param name="targets"></param>

    private void Explosion(Collider[] targets)
    {
        foreach (Collider target in targets)
        {
            Debug.DrawRay(transform.position, ((target.transform.position - transform.position).normalized) * (Vector3.Distance(transform.position, target.transform.position)), Color.yellow);
            //TODO knockBack 
            target.GetComponent<Rigidbody>().velocity = (target.transform.position - transform.position).normalized * (knockBackForce);
        }
    }
}

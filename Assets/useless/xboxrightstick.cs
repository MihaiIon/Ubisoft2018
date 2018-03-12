using UnityEngine;
using System.Collections;

public class xboxrightstick : MonoBehaviour
{
    public float speed = .5f;
    public float turnSpeed = 60;
    private Rigidbody rig;
    public float distance;
    public Transform target;
    public Transform mouseposition;
    public float maxdist;
    // Use this for initialization
    private void Update()
    {
        float angH = Input.GetAxis("Mouse X")*60;
        float angV = Input.GetAxis("Mouse Y")*40;
        transform.localEulerAngles = new Vector3(angV, angH, 0);
    }
    private void LateUpdate()
    {
        transform.localEulerAngles = new Vector3(0, 0, transform.localEulerAngles.z);
    }
}
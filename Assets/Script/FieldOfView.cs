using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour {

    //TODO: sync field of view with lightRadious
    //fieldOfView code taken from the
    //Field of view visualisation series by Sebastian Lague
    public float viewRadius;
    [Range(0,360)]
    public float viewAngle;

    public float widthOfHitBox;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    public List<Transform> visibleTargets = new List<Transform>();
    public Collider[] targetsInViewRadius;

    private void Start()
    {
        StartCoroutine("FindTargestWithDelay", .2f);
    }

    IEnumerator FindTargestWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    void FindVisibleTargets()
    {
        visibleTargets.Clear();
        targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float dstTarget = Vector3.Distance(transform.position, target.position);
                Ray ray = new Ray(transform.position, dirToTarget);
                if (!Physics.Raycast(ray,dstTarget-widthOfHitBox))
                {
                    visibleTargets.Add(target);
                }
            }
        }
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if(!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}

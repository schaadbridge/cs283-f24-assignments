using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeController : MonoBehaviour
{
    public Transform target;
    public Transform lookJoint;
    // public Transform parent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(target.position, lookJoint.position, Color.red);

        Vector3 r = target.forward;
        Vector3 e = target.position - lookJoint.position;

        float rotationAngle = (float) Math.Atan2(Vector3.Magnitude(Vector3.Cross(r, e)), Mathf.Abs(Vector3.Dot(r, r) + Vector3.Dot(r, e)));
        Vector3 rotationAxis = Vector3.Cross(r, e).normalized;

        lookJoint.Rotate(rotationAxis, rotationAngle);
    }
}

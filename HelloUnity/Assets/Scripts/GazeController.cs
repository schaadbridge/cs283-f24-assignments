using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeController : MonoBehaviour
{
    public Transform target;
    public Transform lookJoint;
    public Transform parent;
    Quaternion initialRot;
    // public Transform parent;
    // Start is called before the first frame update
    void Start()
    {
        initialRot = parent.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(parent.position, lookJoint.position, Color.red);
        Debug.DrawLine(target.position, lookJoint.position, Color.red);

        parent.rotation = initialRot;

        Vector3 r = lookJoint.position - parent.position;
        Vector3 e = target.position - lookJoint.position;

        IKRotation(parent, r, e);
        // float rotationAngle = (float) Math.Atan2(Vector3.Cross(r, e).magnitude, Vector3.Dot(r, r) + Vector3.Dot(r, e)) * Mathf.Rad2Deg;
        // // Debug.Log(rotationAngle);
        // Vector3 rotationAxis = Vector3.Cross(r, e).normalized;

        // parent.rotation = Quaternion.AngleAxis(rotationAngle, rotationAxis) * parent.rotation;
    }

    public static void IKRotation(Transform parent, Vector3 r, Vector3 e) {
        float rotationAngle = (float) Math.Atan2(Vector3.Cross(r, e).magnitude, Vector3.Dot(r, r) + Vector3.Dot(r, e)) * Mathf.Rad2Deg;
        // Debug.Log(rotationAngle);
        Vector3 rotationAxis = Vector3.Cross(r, e).normalized;

        parent.rotation = Quaternion.AngleAxis(rotationAngle, rotationAxis) * parent.rotation;
    }
}

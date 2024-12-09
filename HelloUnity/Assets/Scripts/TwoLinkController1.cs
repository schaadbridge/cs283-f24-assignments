using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class TwoLinkController1 : MonoBehaviour
{
    // Should be on cat, should take the hip, knee, and end effector (foot)
    // Start is called before the first frame update
    public Transform sourceJoint;
    public Transform pivotJoint;
    public Transform endEffector;
    // place target atop end effector

    private float l1;
    private float l2;
    private float maxLength;
    public Transform target;
    Quaternion initialRot;
    void Start()
    {
        initialRot = pivotJoint.rotation;

        // Always rotating around y, so can just do simple distances
        l1 = Math.Abs(pivotJoint.position.y - sourceJoint.position.y);
        l2 = Math.Abs(endEffector.position.y - pivotJoint.position.y);
        maxLength = l1 + l2;
    }

    // Update is called once per frame
    void Update()
    {
        // update end effector location
        // we want distance from source to endEffector to equal distance from source to target
        float r =(target.position - sourceJoint.position).magnitude;
        if (r < maxLength) {
            // I want the angle l1 and l2 form to be theta, and I want to rotate about the up axis of the knee
            float theta = (float) Math.Acos(-(r * r - l1 * l1 - l2 * l2) / (-2.0f * l1 * l2)) * Mathf.Rad2Deg;

            pivotJoint.localRotation = Quaternion.AngleAxis(theta, Vector3.up);

            sourceJoint.rotation = initialRot;
            GazeController.IKRotation(sourceJoint, endEffector.position - sourceJoint.position, target.position - endEffector.position - sourceJoint.position);
        }
    }

    void OnDrawGizmos() {
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(sourceJoint.position, pivotJoint.position);
            Gizmos.DrawLine(target.position, pivotJoint.position);
            Gizmos.DrawSphere(target.position, 0.01f);
        }
    }
}

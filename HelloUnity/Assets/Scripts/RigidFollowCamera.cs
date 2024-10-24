using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class RigidFollowCamera : MonoBehaviour
{
  // hDist = horizontal follow distance
  // vDist = vertical follow distance
  public float hDist = 1.0f;
  public float vDist = 0.25f;
  public Vector3 camOffset = new Vector3(0, 0, 0);
  public Transform target;
  // Start is called before the first frame update
  void Start()
  {
      camOffset.Normalize();
  }

  // Update is called once per frame
  void Update()
  {
    // tPos, tUp, tForward = Position, up, and forward vector of target
    Vector3 tPos = target.position;
    Vector3 tUp = target.up;
    Vector3 tForward = target.forward + camOffset;

  // Camera position is offset from the target position
    Vector3 eye = tPos - tForward * hDist + tUp * vDist;

  // The direction the camera should point is from the target to the camera position
    Vector3 cameraForward = tPos - eye;

  // Set the camera's position and rotation with the new values
  // This code assumes that this code runs in a script attached to the camera
    transform.position = eye;
    transform.rotation = Quaternion.LookRotation(cameraForward);   
  }
}

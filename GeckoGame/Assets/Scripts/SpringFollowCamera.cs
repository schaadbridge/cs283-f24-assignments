using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringFollowCamera : MonoBehaviour
{
  // hDist = horizontal follow distance
  // vDist = vertical follow distance
  public float hDist = 0.3f;
  public float vDist = 1.88f;
  public float dampConstant;
  public float springConstant = 0.5f;
    // Start is called before the first frame update
  public Transform target;

  Vector3 actualPosition;

  public Vector3 velocity;
  void Start()
  {
      actualPosition = target.position + new Vector3(-hDist, vDist, 0);
      dampConstant = 2.0f * Mathf.Sqrt(springConstant);
      velocity = new Vector3(0, 0, 0);
  }

  // Update is called once per frame
  void Update()
  {
    // tPos, tUp, tForward = Position, up, and forward vector of target
    Vector3 tPos = target.position;
    Vector3 tUp = target.up;
    Vector3 tForward = target.forward;
    // Camera position is offset from the target position
    Vector3 idealEye = tPos - tForward * hDist + tUp * vDist;

    // The direction the camera should point is from the target to the camera position
    Vector3 cameraForward = tPos - actualPosition;

    // Compute the acceleration of the spring, and then integrate
    Vector3 displacement = transform.position - idealEye;
    Vector3 springAccel = (-springConstant * displacement) - (dampConstant * velocity);

    // Update the camera's velocity based on the spring acceleration
    velocity += springAccel * Time.deltaTime;
    actualPosition += velocity * Time.deltaTime;

    // Set the camera's position and rotation with the new values
    // This code assumes that this code runs in a script attached to the camera
    transform.position = actualPosition;
    transform.rotation = Quaternion.LookRotation(cameraForward);
  }
}

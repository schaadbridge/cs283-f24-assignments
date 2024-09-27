using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
  public Camera camera;
  public GameObject cameraPositions;
  public float duration = 3.0f;
  // List of all the camera transforms
  private List<Transform> transformList = new List<Transform>();
  private int currWaypointIdx = 0;
  private Transform start;
  private Transform end;
  // Interpolant between start and end
  private float u = 1.0f;

  // Start is called before the first frame update
  void Start()
  {
    foreach (Transform child in cameraPositions.transform) {
      transformList.Add(child);
    }
    start = transformList[0];
    end = transformList[0];
    Debug.Log("End position is" + end.position);
    Debug.Log("Current camera position is" + camera.transform.position);
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetKeyDown(KeyCode.N)) {
      Debug.Log ("Key N was pressed. Changing cameras.");

      // Update start position and end position
      currWaypointIdx = (currWaypointIdx + 1) % transformList.Count;
      start = camera.transform;
      end = transformList[currWaypointIdx];
      u = 0.0f;
    }
    // If camera not at end position:
    if (u < 1) {
      u += (Time.deltaTime / duration);
      camera.transform.rotation = Quaternion.Slerp(start.rotation, end.rotation, u);
      camera.transform.position = Vector3.Lerp(start.position, end.position, u);
    }
    if (u > 1) {
      camera.transform.rotation = end.rotation;
      camera.transform.position = end.position;
      u = 1;
    }
  }
}

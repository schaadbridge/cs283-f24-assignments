using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using UnityEngine;

public class FollowPathLinear : MonoBehaviour
{
  public GameObject poiParent;
  // List of all the POI transforms
  private List<Transform> transformList = new List<Transform>();
  private int currPOIIdx = 0;
  public Transform start;
  public Transform end;
  public float duration = 3.0f;
  float timer = 0;

  Vector3[] positions;

  // Start is called before the first frame update
  void Start()
  {
    positions = new Vector3[poiParent.transform.childCount];
    int i = 0;
    foreach (Transform child in poiParent.transform) {
      transformList.Add(child);
      positions[i++] = child.position;
    }
    start = transformList[0];
    end = transformList[0];
    StartCoroutine(DoLerp());
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Space))
    {
      currPOIIdx = (currPOIIdx + 1) % transformList.Count;
      start = transform;
      end = transformList[currPOIIdx];
      StartCoroutine(DoLerp());
      // change rotatiom
      Vector3 _rotation = (end.position - start.position).normalized;
      transform.rotation = Quaternion.LookRotation(_rotation, Vector3.up);
    }
  }

  IEnumerator DoLerp()
  {
    for (timer = 0; timer < duration; timer += Time.deltaTime)
    {
      float u = timer / duration;
      transform.position = Vector3.Lerp(start.position, end.position, u);
      yield return null;
    }
  }

  void OnDrawGizmos()
  {
    Gizmos.color = Color.blue;
    Gizmos.DrawLineStrip(positions, true);
  }
}

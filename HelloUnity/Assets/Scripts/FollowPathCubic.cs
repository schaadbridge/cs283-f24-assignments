using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;

public class FollowPathCubic : MonoBehaviour
{
  public GameObject poiParent;
  // List of all the POI transforms
  public Vector3[] ctrlPoints;
  public List<Transform> transformList = new List<Transform>();
  private int currPOIIdx = 0;
  public Transform start;
  public Transform end;
  private Vector3 b0;
  private Vector3 b1;
  private Vector3 b2;
  private Vector3 b3;
  // If true, compute using DeCasteljau. If false, use polynomial.
  public Boolean DeCasteljau = false;
  Vector3[] positions;
  public float duration = 3.0f;

  // Start is called before the first frame update
  void Start()
  {
    ctrlPoints = new Vector3[2 * (poiParent.transform.childCount - 1)];
    positions = new Vector3[poiParent.transform.childCount];
    int i = 0;
    foreach (Transform child in poiParent.transform) {
      Debug.Log("i: "+ i + "child position " + child.position);
      transformList.Add(child);
      positions[i++] = child.position;
    }

    ctrlPoints[0] = positions[0] + ((positions[1] - positions[0]) / 6);
    ctrlPoints[^1] = positions[^1] - ((positions[^1] - positions[^2]) / 6);
    for (int j = 0; j < positions.Length; j++)
    {
      if (j > 0) {
        ctrlPoints[j * 2] = positions[j] + ((positions[j + 1] - positions[j - 1]) / 6);
      }
      if (j < positions.Length - 2) {
        ctrlPoints[j * 2 + 1] = positions[j + 1] - ((positions[j + 2] - positions[j]) / 6);
      }
    }
  }

  // Update is called once per frame
  void Update()
  {
      if (Input.GetKeyDown(KeyCode.Space))
      {
        start = transformList[currPOIIdx];
        currPOIIdx = (currPOIIdx + 1) % transformList.Count;
        end = transformList[currPOIIdx];

        // Assign control points -- can prob replace this whole part!
        b0 = start.position;
        b1 = ctrlPoints[currPOIIdx * 2];
        b2 = ctrlPoints[currPOIIdx * 2 + 1];
        b3 = end.position;
        
        Debug.Log("currPOIIdx: " + currPOIIdx + " ctrl1: " + b1 + " ctrl2: " + b2);
        if (DeCasteljau) {
          StartCoroutine(DoDecasteljau());
        } else 
        {
          StartCoroutine(DoPolynomial());
        }
      }
  }

  IEnumerator DoPolynomial() 
  {
    for (float timer = 0; timer < duration; timer += Time.deltaTime)
    {
      float u = timer / duration;
      Vector3 temp = transform.position;
      transform.position = ((float) Math.Pow(1 - u, 3) * b0) + 
                            ((float) (3 * u * Math.Pow(1 - u, 2)) * b1) + 
                            ((float) (3 * Math.Pow(u, 2) * (1 - u)) * b2) + 
                            ((float) Math.Pow(u, 3) * b3);
      // change rotation
      Vector3 _rotation = (transform.position - temp).normalized;
      transform.rotation = Quaternion.LookRotation(_rotation, Vector3.up);
      yield return null;
    }
  }

  IEnumerator DoDecasteljau() 
  {
    for (float timer = 0; timer < duration; timer += Time.deltaTime)
    {
      float u = timer / duration;
      Vector3 temp = transform.position;
      Vector3 b01 = Vector3.Lerp(b0, b1, u);
      Vector3 b12 = Vector3.Lerp(b1, b2, u);
      Vector3 b23 = Vector3.Lerp(b2, b3, u);

      Vector3 b02 = Vector3.Lerp(b01, b12, u);
      Vector3 b13 = Vector3.Lerp (b12, b23, u);

      transform.position = Vector3.Lerp(b02, b13, u);
      // change rotation
      Vector3 _rotation = (transform.position - temp).normalized;
      transform.rotation = Quaternion.LookRotation(_rotation, Vector3.up);
      yield return null;
    }
  }

  void OnDrawGizmos()
  {
    Gizmos.color = Color.blue;
    Gizmos.DrawLineStrip(positions, false);
    foreach (Vector3 p in positions)
    {
      Gizmos.DrawSphere(p, 0.2f);
    }
    foreach (Vector3 p in ctrlPoints)
    {
        Gizmos.DrawSphere(p, 0.2f);
    }
  }
}

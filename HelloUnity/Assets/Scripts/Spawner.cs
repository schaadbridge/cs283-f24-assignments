using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float radius = 5.0f;
    public GameObject templateObject;
    public int maxSpawn = 10;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < maxSpawn; i++) {
            float x_val = UnityEngine.Random.Range(transform.position.x - radius, transform.position.x + radius);
            float z_val = UnityEngine.Random.Range(transform.position.z - radius, transform.position.z + radius);
            Instantiate(templateObject, new Vector3(x_val, transform.position.y, z_val), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}

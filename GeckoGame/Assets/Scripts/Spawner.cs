using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public float radius = 30.0f;
    public GameObject templateObject;
    public int maxSpawn = 30;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < maxSpawn; i++) {
            PlaceObjectRandom();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public void PlaceObjectRandom()
    {
        Vector3 target;
        RandomPointOnTerrain(transform.position, radius, out target);
        if (target != Vector3.zero)
        {
            Instantiate(templateObject, target, Quaternion.identity, transform);
        }
    }

    private void RandomPointOnTerrain(Vector3 position, float x, out Vector3 target)
    {
        Vector2 delta = Random.insideUnitCircle * x;
        Vector3 newLocation = new Vector3(position.x + delta.x, position.y, position.z + delta.y);
        NavMeshHit hit;
        // Once a new target is selected on the navmesh, return that position
        if (NavMesh.SamplePosition(newLocation, out hit, 1.0f, NavMesh.AllAreas))
        {
            target = hit.position;
            return;
        }
        else
        {
            target = Vector3.zero;
        }
        return;
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.AI;

public class Wander : MonoBehaviour
{
    public Animator m_Animator;
    public CharacterController m_controller;
    public float movementSpeed = 0.5f;
    public NavMeshAgent m_Agent;
    // Radius of circle around agent from which random new positions are chosen 
    public float radius = 5.0f;
    public Vector3 destination;
    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
        m_controller = gameObject.GetComponent<CharacterController>();
        m_Agent = gameObject.GetComponent<NavMeshAgent>();
        m_Agent.speed = movementSpeed;
        m_Agent.acceleration = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        m_Agent.speed = movementSpeed;
        // if within acceptable distance to current target
        if (m_Agent.remainingDistance < 0.2f) {
            // select new target
            if (RandomPoint(transform.position, radius, out destination)) {
                m_Agent.SetDestination(destination);            
            }    
        }

        // Sends the value from the horizontal axis input to the animator. Change the settings in the
        // Animator to define when the character is walking or running
        m_Animator.SetFloat("movementSpeed", movementSpeed);
    }

    private bool RandomPoint(Vector3 position, float radius, out Vector3 result) {
        Vector2 delta = Random.insideUnitCircle * radius;
        Debug.Log("delta: " + delta);
        Vector3 newLocation = new Vector3(transform.position.x + delta.x, transform.position.y, transform.position.z + delta.y);
        Debug.Log("new location: " + newLocation);
        NavMeshHit hit;
        if (NavMesh.SamplePosition(newLocation, out hit, 1.0f, NavMesh.AllAreas)){
            result = hit.position;
            return true;
        }
        result = Vector3.zero;
        return false;
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(destination, 0.1f);
    }
}

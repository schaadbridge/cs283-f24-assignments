using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using BTAI;

public class WanderBehavior : MonoBehaviour
{
    public Transform wanderRange;  // Set to a sphere
    private Root m_btRoot = BT.Root(); 

    void Start()
    {
        BTNode moveTo = BT.RunCoroutine(MoveToRandom);

        Sequence sequence = BT.Sequence();
        sequence.OpenBranch(moveTo);

        m_btRoot.OpenBranch(sequence);
    }

    void Update()
    {
        m_btRoot.Tick();
    }

    IEnumerator<BTState> MoveToRandom()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();

        Vector3 target;
        Utils.RandomPointOnTerrain(
          wanderRange.position, wanderRange.localScale.x, out target);
        if (target != Vector3.zero)
        {
            agent.SetDestination(target);
        }

       // wait for agent to reach destination
       while (agent.remainingDistance > 0.1f)
       {
          yield return BTState.Continue;
       }

       yield return BTState.Success;
    }
}

internal class Utils
{
    internal static void RandomPointOnTerrain(Vector3 position, float x, out Vector3 target)
    {
        Vector3 delta = Random.insideUnitSphere * x;
        Vector3 newLocation = position + delta;
        Debug.Log("new location: " + newLocation);
        NavMeshHit hit;
        // Once a new target is selected on the navmesh, return that position
        if (NavMesh.SamplePosition(newLocation, out hit, 1.0f, NavMesh.AllAreas)){
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
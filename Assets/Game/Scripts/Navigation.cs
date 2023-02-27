using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Navigation : MonoBehaviour
{
    private bool followTransform = true;
    private Transform destination;
    private Vector3 destinationPoint;
    private NavMeshAgent navMeshAgent;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis= false;
        destination = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        SetAgentDestination();
    }

    public void SetTarget(Transform newTarget)
    {
        followTransform = true;
        destination = newTarget;
    }

    public void SetTarget(Vector3 newTarget)
    {
        followTransform = false;
        destinationPoint = newTarget;
    }

    private void SetAgentDestination ()
    {
        var position = transform.position;

        if (followTransform)
        {
            position.x = destination.position.x;
            position.y = destination.position.y;
        }
        else
        {

            position.x = destinationPoint.x;
            position.y = destinationPoint.y;
        }

        navMeshAgent.SetDestination(position);
    }
}

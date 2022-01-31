using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerNav : MonoBehaviour
{
    NavMeshAgent agent;
    Transform target;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        StartCoroutine(UpdateDestination());
    }

    IEnumerator UpdateDestination()
    {
        if (target != null) {
            agent.SetDestination(target.position);
            RotateTowardTarget();
        }
        yield return null;
    }

    public void navigate(Vector3 destination)
    {
        if (agent.GetComponentInParent<PlayerStats>().canMove)
            agent.SetDestination(destination);
    }

    void RotateTowardTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);
    }
}

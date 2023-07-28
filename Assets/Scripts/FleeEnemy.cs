using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FleeEnemy : MonoBehaviour
{
    Transform target;
    UnityEngine.AI.NavMeshAgent agent;
    public float fleeSpeed = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();    
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 target2enemy = transform.position - target.position;
        agent.SetDestination(transform.position + target2enemy.normalized * fleeSpeed);
    }
}

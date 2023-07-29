using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FleeEnemy : MonoBehaviour
{
    Transform target;
    UnityEngine.AI.NavMeshAgent agent;
    public float fleeSpeed = 5.0f;
    public float giveawayLife = 3.0f;

    public int enemyHealth = 1;
    int health;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();    
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        target = GameObject.FindWithTag("Player").transform;

        health = enemyHealth;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 target2enemy = transform.position - target.position;
        agent.SetDestination(transform.position + target2enemy.normalized * fleeSpeed);
        animator.SetFloat("X Direction", target2enemy.x);
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.CompareTag("Bullet")) {
            health -= 1;
            if (health == 0) {
                PlayerHealth phealth = target.gameObject.GetComponent<PlayerHealth>();
                phealth.AddLife(giveawayLife);
                Destroy(gameObject);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NavMeshPlus.Components;

public class FleeEnemy : MonoBehaviour
{
    Transform target;
    UnityEngine.AI.NavMeshAgent agent;
    public float fleeSpeed = 5.0f;
    public float giveawayLife = 3.0f;

    public int enemyHealth = 1;
    int health;
    Animator animator;

    
    public GameObject deathAnimation;
    public float deathAnimationdelay = 0.7f;

    public SpriteRenderer renderer;

    public float roamingInterval = 3.0f;
    public float fleeDistance;
    public float roamingRadius = 30.0f;
    Vector3 randomPosition;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();    
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        target = GameObject.FindWithTag("Player").transform;

        health = enemyHealth;
        animator = GetComponent<Animator>();
        StartCoroutine(NextDestination());
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 target2enemy = transform.position - target.position;
        if (target2enemy.magnitude <= fleeDistance) {
            agent.SetDestination(transform.position + target2enemy.normalized * fleeSpeed);
            animator.SetFloat("X Direction", target2enemy.x);
        }
        else {
            // set to random position
            agent.SetDestination(randomPosition);
            animator.SetFloat("X Direction", -target2enemy.x);
        }
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.CompareTag("Bullet")) {
            health -= 1;
            if (health == 0) {
                PlayerHealth phealth = target.gameObject.GetComponent<PlayerHealth>();
                phealth.AddLife(giveawayLife);
                //Destroy(gameObject);
                deathAnimation.SetActive(true);
                StartCoroutine(DelayDestroy());
            }
        }
    }

    IEnumerator NextDestination() {
        while (true) {
            Vector3 newPositionOffset = Random.insideUnitSphere * roamingRadius;
            newPositionOffset.z = 0;

            Vector3 newDest = transform.position + newPositionOffset;
 
            NavMeshHit navHit;
    
            NavMesh.SamplePosition (newDest, out navHit, roamingRadius, NavMesh.AllAreas);
            randomPosition = navHit.position;
    
            yield return new WaitForSeconds(roamingInterval);
        }
    }

    IEnumerator DelayDestroy() {
        renderer.enabled = false;
        yield return new WaitForSeconds(deathAnimationdelay);
        Destroy(gameObject);
    }
}

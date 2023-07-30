using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseEnemy : MonoBehaviour
{
    public int enemyHealth = 1;
    int health;
    Transform target;
    NavMeshAgent agent;

    public GameObject deathAnimation;
    public float deathAnimationdelay = 1.0f;

    public SpriteRenderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();    
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        target = GameObject.FindWithTag("Player").transform;

        health = enemyHealth;
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.transform.position);
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.CompareTag("Bullet")) {
            health -= 1;
            if (health == 0) {
                //Destroy(gameObject);
                deathAnimation.SetActive(true);
                StartCoroutine(DelayDestroy());
            }
        }
    }

    IEnumerator DelayDestroy() {
        renderer.enabled = false;
        yield return new WaitForSeconds(deathAnimationdelay);
        Destroy(gameObject);
    }
}

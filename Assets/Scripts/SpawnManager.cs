using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NavMeshPlus.Components;

public class SpawnManager : MonoBehaviour
{
    public NavMeshSurface navSurface;
    public GameObject indicator;
    public PolygonCollider2D worldBound;

    public GameObject chaseEnemyPrefab;
    public GameObject fleeEnemyPrefab;

    public int chaseEnemyCount = 5;
    public int fleeEnemyCount = 5;

    public float spawnInterval = 3.0f;
    bool isInGame;

    public PlayerHealth playerHealth;

    void Start()
    {
        playerHealth.OnPlayerDead += () => {
            SetIsInGame(false);
            DeleteAllEnemies(); 
        };
    }

    public void SetIsInGame(bool state) {
        isInGame = state;
    }

    private IEnumerator RepeatSpawnEnemies() {
        isInGame = true;
        while (isInGame == true) {
            SpawnChaseEnemy();
            SpawnFleeEnemy();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public void Restart() {
        StartCoroutine(RepeatSpawnEnemies());
    }

    public void DeleteAllEnemies() {
        GameObject[] tmp = GameObject.FindGameObjectsWithTag("ChaseEnemy");
        foreach (GameObject obj in tmp) {
            Destroy(obj);
        }
        tmp = GameObject.FindGameObjectsWithTag("FleeEnemy");
        foreach (GameObject obj in tmp) {
            Destroy(obj);
        }
    }

    void SpawnEnemy(GameObject prefab, int count) {
        int spawned = 0;
        Bounds bounds = worldBound.bounds;
        while (spawned <= count) {
            Vector3 randomPoint = new Vector3(
                Random.Range(bounds.min.x, bounds.max.x),
                Random.Range(bounds.min.y, bounds.max.y),
                Random.Range(bounds.min.z, bounds.max.z)
            ); 

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, Mathf.Infinity, NavMesh.AllAreas))
            {
                Instantiate(prefab, hit.position, indicator.transform.rotation);
                spawned += 1;
            }
        }
    }

    void SpawnChaseEnemy() {
        GameObject[] tmp = GameObject.FindGameObjectsWithTag("ChaseEnemy");
        SpawnEnemy(chaseEnemyPrefab, chaseEnemyCount - tmp.Length);
    }

    void SpawnFleeEnemy() {
        GameObject[] tmp = GameObject.FindGameObjectsWithTag("FleeEnemy");
        SpawnEnemy(fleeEnemyPrefab, fleeEnemyCount - tmp.Length);
    }

}

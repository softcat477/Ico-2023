using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    // Destroy gameobject after aliveTime;
    public float aliveTime = 5f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CountDown());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator CountDown() {
        yield return new WaitForSeconds(aliveTime);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collider) {
        Destroy(gameObject);
    }
}

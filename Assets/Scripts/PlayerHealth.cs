using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxLife = 10.0f;
    [SerializeField] private float currentLife;
    // Start is called before the first frame update

    public delegate void PlayerDeadDelegate();
    public event PlayerDeadDelegate OnPlayerDead;

    public delegate void CurrentLifeChangedDelegate(float newLife);
    public event CurrentLifeChangedDelegate OnCurrentLifeChanged;

    Rigidbody2D rb;
    
    float dtime = 0.1f;

    public ShakeCamera shakeCamera;
    void Start()
    {
        StartGame();
        rb = GetComponent<Rigidbody2D>();
    }

    public void AddLife(float dlife, bool shake = true) {
        currentLife += dlife;
        OnCurrentLifeChanged?.Invoke(currentLife);

        if (shake)
            shakeCamera.StartShake();
    }

    public void StartGame() {
        currentLife = maxLife;
        OnCurrentLifeChanged?.Invoke(currentLife);
        StartCoroutine(CountDown());
    }

    private IEnumerator CountDown() {
        while (currentLife >= 0.0f) {
            yield return new WaitForSeconds(dtime);
            AddLife(-dtime, false);
        }
        OnPlayerDead?.Invoke();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("ChaseEnemy"))
        {
            AddLife(-0.5f);
        }
    }
}

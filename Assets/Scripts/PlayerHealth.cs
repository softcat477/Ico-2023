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
    
    float dtime = 0.1f;
    void Start()
    {
        StartGame();
    }

    public void AddLife(float dlife) {
        currentLife += dlife;
    }

    private void StartGame() {
        currentLife = maxLife;
        StartCoroutine(CountDown());
    }

    private IEnumerator CountDown() {
        while (currentLife >= 0.0f) {
            yield return new WaitForSeconds(dtime);
            currentLife -= dtime;
        }
        Debug.Log("Player dead.");
        OnPlayerDead?.Invoke();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject GameOverUI;
    public static GameManager instance;
    public float TimeLeft;
    public float respawnPlayerInterval = 1.0f;

    public GameObject player;

    Vector3 playerRespawnPosition;

    private void Awake() {
        if (instance != null) {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    private void Start() {
        playerRespawnPosition = player.transform.position;
        player.GetComponent<PlayerHealth>().OnPlayerDead += DoGameOverBehavior;
        player.GetComponent<PlayerHealth>().OnCurrentLifeChanged += (float newLife) => {TimeLeft = newLife;};
    }

    public IEnumerator RespawnPlayer(float coolDown) {
        yield return new WaitForSeconds(coolDown);
        player.transform.position = playerRespawnPosition;
        HideGameOverUI();
        player.GetComponent<PlayerHealth>().StartGame();
    }

    public void ShowGameOverUI()
    {
        GameOverUI.SetActive(true);
    }

    public void HideGameOverUI()
    {
        GameOverUI.SetActive(false);
    }

    public void DoGameOverBehavior()
    {
        //Time.timeScale = 0;
        ShowGameOverUI();
        StartCoroutine(RespawnPlayer(respawnPlayerInterval));
    }
}

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
    [SerializeField] Timer timer;

    Vector3 playerRespawnPosition;

    public SpawnManager spawnManager;

    private bool bPlayerDead = false;

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
        player.GetComponent<PlayerHealth>().OnCurrentLifeChanged += (float newLife) => {
            TimeLeft = newLife;
            timer.UpdateTimer(newLife);
        };
        spawnManager.Restart();
    }

    private void Update() {
        // add conditional here for pressing "R" for restart
        if (Input.GetKeyDown("r") && bPlayerDead)
        {
            OnPlayerRespawnWasPressed();
        }
        // if (getkeydown R) or with new input system.
        // also check if player is dead
        // if bPlayerDead is true
            //...
            //OnPlayerRespawnWasPressed();
    }

    public IEnumerator RespawnPlayer(float coolDown) {
        //reset time scale to 1
        Time.timeScale = 1;
        yield return new WaitForSeconds(coolDown);

        player.transform.position = playerRespawnPosition;
        HideGameOverUI();
        player.GetComponent<PlayerHealth>().StartGame();

        ResetPlayerConditions();
        spawnManager.Restart();
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
        Time.timeScale = 0;
        ShowGameOverUI();
        DoDisablePlayerControls();
        bPlayerDead = true;
    }

    public void OnPlayerRespawnWasPressed()
    {
        StartCoroutine(RespawnPlayer(respawnPlayerInterval));
    }

    public void DoDisablePlayerControls()
    {
        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponent<PlayerAim>().enabled = false;
    }

    public void DoEnablePlayerControls()
    {
        player.GetComponent<PlayerMovement>().enabled = true;
        player.GetComponent<PlayerAim>().enabled = true;
    }

    public void ResetPlayerConditions()
    {
        DoEnablePlayerControls();
        bPlayerDead = false;
    }
}

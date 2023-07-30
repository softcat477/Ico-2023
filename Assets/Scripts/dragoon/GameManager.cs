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
    public GameObject PowerUpAnim;

    Vector3 playerRespawnPosition;

    public SpawnManager spawnManager;

    private bool bPlayerDead = false;

    public AudioSource gameManagerBGM;
    public AudioSource normalBGM;
    public AudioSource gameOverBGM;
    public AudioSource tickingBGM;

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

        if (GameManager.instance.TimeLeft >= 60)
        {
            PlayNormalBGM();
        }
        else if (GameManager.instance.TimeLeft < 60 && !bPlayerDead)
        {
            PlayTickingBGM();
        } else if (GameManager.instance.TimeLeft <= 0f)
        {
            PlayGameOverBGM();
        }
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
        PlayGameOverBGM();
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

    public void PlayNormalBGM()
    {
        //pause all audio sources except normal
        if (normalBGM.isPlaying == false)
        {
            PlayClipFromAudioSource(normalBGM);
        }
        if (tickingBGM.isPlaying == true)
        {
            PauseClipFromAudioSource(tickingBGM);
        }
        if (gameOverBGM.isPlaying == true)
        {
            PauseClipFromAudioSource(gameOverBGM);
        }
    }

    public void PlayTickingBGM()
    {
        //pause all audio sources except ticking
        if (normalBGM.isPlaying == true)
        {
            PauseClipFromAudioSource(normalBGM);
        }
        if (gameOverBGM.isPlaying == true)
        {
            PauseClipFromAudioSource(gameOverBGM);
        }
        if (tickingBGM.isPlaying == false)
        {
            PlayClipFromAudioSource(tickingBGM);
        }
    }

    public void PlayGameOverBGM()
    {
        //pause all audio sources except gameover
        if (normalBGM.isPlaying == true)
        {
            PauseClipFromAudioSource(normalBGM);
        }
        if (tickingBGM.isPlaying == true)
        {
            PauseClipFromAudioSource(tickingBGM);
        }
        if (gameOverBGM.isPlaying == false)
        {
            PlayClipFromAudioSource(gameOverBGM);
        }
    }

    public void PlayClipFromAudioSource(AudioSource audioSource)
    {
        audioSource.Play();
    }

    public void PauseClipFromAudioSource(AudioSource audioSource)
    {
        audioSource.Pause();
    }

    public void PlayUIPowerUp()
    {
        GameObject powerUp = Instantiate(PowerUpAnim, player.transform.position, player.transform.rotation);
        // PowerUpAnim.transform.position = player.transform.position;
        powerUp.GetComponent<PowerUpUIAnim>().OnPlayerDefeatedBoss();
    }
}

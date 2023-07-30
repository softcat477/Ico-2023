using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public bool TimerOn = false;

    public TMP_Text TimerText;

    // Start is called before the first frame update
    void Start()
    {
        // TimerOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (TimerOn)
        {
            if (GameManager.instance.TimeLeft >= 60)
            {
                GameManager.instance.PlayNormalBGM();
            }
            else if (GameManager.instance.TimeLeft < 60 && GameManager.instance.TimeLeft > 0.5f)
            {
                GameManager.instance.PlayTickingBGM();
            } else if (GameManager.instance.TimeLeft <= 0f)
            {
                GameManager.instance.PlayGameOverBGM();
            }
                // GameManager.instance.TimeLeft -= Time.deltaTime;
                // UpdateTimer(GameManager.instance.TimeLeft);
            // else
            // {
                // GameManager.instance.TimeLeft = 0;
                // UpdateTimer(GameManager.instance.TimeLeft);
                // TimerOn = false;
                // GameManager.instance.ShowGameOverUI();
                // GameManager.instance.DoGameOverBehavior();
            // }
        }
    }

    public void UpdateTimer(float currentTime)
    {
        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        minutes = Mathf.Clamp(minutes, 0.0f, minutes);
        seconds = Mathf.Clamp(seconds, 0.0f, seconds);

        TimerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}

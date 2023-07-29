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
        //TimerOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        // if (TimerOn)
        // {
        //     if (GameManager.instance.TimeLeft > 0)
        //     {
        //         GameManager.instance.TimeLeft -= Time.deltaTime;
        //         UpdateTimer(GameManager.instance.TimeLeft);
        //     }
        //     else
        //     {
        //         GameManager.instance.TimeLeft = 0;
        //         UpdateTimer(GameManager.instance.TimeLeft);
        //         TimerOn = false;
        //         GameManager.instance.ShowGameOverUI();
        //         GameManager.instance.DoGameOverBehavior();
        //     }
        // }
    }

    public void UpdateTimer(float currentTime)
    {
        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        TimerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}

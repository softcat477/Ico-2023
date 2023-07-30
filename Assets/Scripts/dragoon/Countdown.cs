using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Countdown : MonoBehaviour
{
    public TMP_Text TimerText;
    public float currentTime = 3.0f;
    public float resetTime = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTime > 0.0f)
        {
            UpdateTimer();
        } else if (currentTime <= 0.1f) {
            UpdateTimerToGo();
        }
    }

    public void UpdateTimer()
    {
        currentTime -= Time.deltaTime;
        TimerText.text = Mathf.Ceil(currentTime).ToString();
    }

    public void UpdateTimerToGo()
    {
        TimerText.text = "IGNITE!!";
    }

    private void OnEnable() {
        currentTime = resetTime;
    }

    private void OnDisable() {
        currentTime = resetTime;
    }
}

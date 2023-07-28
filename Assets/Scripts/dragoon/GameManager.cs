using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject GameOverUI;
    public static GameManager instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowGameOverUI()
    {
        Debug.Log("show game over");
        GameOverUI.SetActive(true);
    }

    public void DoGameOverBehavior()
    {
        Time.timeScale = 0;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DoLoadScene(string sceneToLoad)
    {
        // Debug.Log(sceneToLoad);
        SceneManager.LoadScene(sceneToLoad);
    }
}

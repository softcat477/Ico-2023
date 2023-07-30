using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    public void OnButtonClickChangeScene(string sceneToGo)
    {
        SceneManager.LoadScene(sceneToGo);
        // SceneManager.LoadScene(sceneToGo, LoadSceneMode.Additive);
    }

    public void OnButtonClickUnloadScene(string sceneToUnload)
    {
        SceneManager.UnloadSceneAsync(sceneToUnload);
    }
}

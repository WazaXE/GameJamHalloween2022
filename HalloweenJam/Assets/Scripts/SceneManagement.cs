using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public void LoadScene(int sceneIndex) {
        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
    }

    public void LoadNextScene() {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        LoadScene(sceneIndex + 1);
    }

    public void LoadPreviousScene() {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        LoadScene(sceneIndex - 1);
    }

    public void Quit() {
        Application.Quit();
    }
}

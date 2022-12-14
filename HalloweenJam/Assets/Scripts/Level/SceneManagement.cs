using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public void LoadScene(int sceneIndex) {
        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
    }

    public void ReloadScene() {
        LoadScene(GetSceneIndex());
    }

    public void LoadNextScene() {
        int sceneIndex = GetSceneIndex();
        LoadScene(sceneIndex + 1);
    }

    public void LoadPreviousScene() {
        int sceneIndex = GetSceneIndex();
        LoadScene(sceneIndex - 1);
    }

    private int GetSceneIndex() {
        return SceneManager.GetActiveScene().buildIndex;
    }

    public void Quit() {
        Application.Quit();
    }
}

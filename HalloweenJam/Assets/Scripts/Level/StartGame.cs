using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    void Start() {
        GetComponent<SceneManagement>().LoadNextScene();
    }
}

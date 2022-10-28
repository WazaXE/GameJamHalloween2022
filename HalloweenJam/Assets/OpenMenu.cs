using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        GameStateManager.Instance.OnGameStateChange += OnGameStateChange;
    }

    private void OnGameStateChange(GameState gameState)
    {
        switch(gameState)
        {
            case GameState.Gameplay:
                pauseMenu.SetActive(false);
                break;
            case GameState.Paused:
                pauseMenu.SetActive(true);
                break;
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject upgradeMenu;

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
                upgradeMenu.SetActive(false);
                break;
            case GameState.Upgrade:
                upgradeMenu.SetActive(true);
                pauseMenu.SetActive(false);
                break;
        }

    }
    public void ResumeGame()
    {
        GameStateManager.Instance.SetState(GameState.Gameplay);
    }
}

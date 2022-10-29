using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseManager : MonoBehaviour
{
    //[SerializeField] private KeyCode pauseKey;

    public GameObject pauseFirstButton;

    void Update()
    {
        if(Input.GetButtonDown("Escape")) {
            GameState currentState = GameStateManager.Instance.CurrentgameState;
            if (currentState == GameState.Gameplay)
            {

                //Clear selected object

                EventSystem.current.SetSelectedGameObject(null);

                //Set new selected object

                EventSystem.current.SetSelectedGameObject(pauseFirstButton);

                GameStateManager.Instance.SetState(GameState.Paused);


            }

            else GameStateManager.Instance.SetState(GameState.Gameplay);
        }
    }

    public void SetGameState(GameState gameState) {
        GameStateManager.Instance.SetState(gameState);
    }
}

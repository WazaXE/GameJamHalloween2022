using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private KeyCode pauseKey;

    void Update()
    {
        if(Input.GetKeyDown(pauseKey)) {
            GameState currentState = GameStateManager.Instance.CurrentgameState;
            if (currentState == GameState.Gameplay) GameStateManager.Instance.SetState(GameState.Paused);
            else GameStateManager.Instance.SetState(GameState.Gameplay);
        }
    }
}

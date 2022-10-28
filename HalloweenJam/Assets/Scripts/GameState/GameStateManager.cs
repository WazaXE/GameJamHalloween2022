using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager {
    private static GameStateManager instance;
    public static GameStateManager Instance {
        get {
            if (instance == null)
                instance = new GameStateManager();
            return instance;
        }
    }

    public GameState CurrentgameState { get; private set; }

    public delegate void GameStateChangeHandler(GameState newGameState);
    public event GameStateChangeHandler OnGameStateChange;

    private GameStateManager() { }

    public void SetState(GameState newGameState) {
        if (newGameState == CurrentgameState) return;

        CurrentgameState = newGameState;
        OnGameStateChange?.Invoke(newGameState);
        Debug.Log($"Current GameState: {newGameState}");
    }
}

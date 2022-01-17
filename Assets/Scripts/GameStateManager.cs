using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager
{

    private static GameStateManager instance;

    public static GameStateManager Instance
    {
        get 
        { 
            if(instance == null)
            {
                instance = new GameStateManager();
            }

            return instance; 
        }
    }

    public GameState CurrentGameState { get; private set; }

    public delegate void GameStateChangeHandler(GameState newGameState);
    public event GameStateChangeHandler onGameStateChanged;

    private GameStateManager()
    {

    }

    public void SetState(GameState newGameState)
    {
        if(newGameState == CurrentGameState)
            return;

        CurrentGameState = newGameState;
        onGameStateChanged?.Invoke(newGameState);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuOpen : MonoBehaviour
{
    public GameObject m_PauseMenu;

    void Awake()
    {
        GameStateManager.Instance.onGameStateChanged += OnGameStateChanged;

        OnGameStateChanged(GameStateManager.Instance.CurrentGameState);
    }

    void OnDestroy()
    {
        GameStateManager.Instance.onGameStateChanged -= OnGameStateChanged;
    }

    private void OnGameStateChanged(GameState newGameState)
    {
        m_PauseMenu.SetActive(newGameState == GameState.Paused);
    }
}

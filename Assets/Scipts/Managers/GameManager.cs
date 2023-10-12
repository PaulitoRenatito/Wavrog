using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public enum GameState
    {
        Paused,
        Running,
        Ended
    }

    [ReadOnly] [SerializeField] private GameState state;
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        state = GameState.Running;
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
        GameInput.Instance.OnOpenInventoryAction += GameInput_OnOpenInventoryAction;
        WaveManager.Instance.OnLastEnemyDefeated += WaveManager_OnLastEnemyDefeated;
    }

    private void GameInput_OnPauseAction(object sender, EventArgs e)
    {
        switch (state)
        {
            case GameState.Running:
                state = GameState.Paused;
                Time.timeScale = 0f;
                break;
            case GameState.Paused:
                state = GameState.Running;
                Time.timeScale = 1f;
                break;
        }
    }
    
    private void GameInput_OnOpenInventoryAction(object sender, EventArgs e)
    {
        switch (state)
        {
            case GameState.Running:
                state = GameState.Paused;
                Time.timeScale = 0f;
                break;
            case GameState.Paused:
                state = GameState.Running;
                Time.timeScale = 1f;
                break;
        }
    }

    private void WaveManager_OnLastEnemyDefeated(object sender, EventArgs e)
    {
        Time.timeScale = 0.5f;
        FunctionTimer.Create(() => { Time.timeScale = 1f; }, 1f);
    }
}

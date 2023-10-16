using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameState gameState;

    public static event Action<GameState> OnGameStateChanged;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        ChangeState(GameState.GenerateGrid);
    }

    public void ChangeState(GameState newState)
    {
        gameState = newState;
        OnGameStateChanged?.Invoke(newState);
        Debug.Log("---!---");
        switch (newState)
        {
            case GameState.GenerateGrid:
                GridManager.instance.GenerateGrid();
                ChangeState(GameState.HeroesTurn);
                break;
            case GameState.SpawnHeroes:
                //UnitManager.Instance.SpawnHeroes();
                break;
            case GameState.SpawnEnemies:
                //UnitManager.Instance.SpawnEnemies();
                break;
            case GameState.HeroesTurn:
                Debug.Log("State::HeroesTurn");
                break;
            case GameState.EnemiesTurn:
                break;
            case GameState.HeroMove:
                Debug.Log("State::HeroesMoves");
                break;
            default:
                Debug.Log("State::Undefined");
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        //OnGameStateChanged?.Invoke(newState);
    }
}

public enum GameState
{
    GenerateGrid = 0,
    SpawnHeroes = 1,
    SpawnEnemies = 2,
    HeroesTurn = 3,
    EnemiesTurn = 4,
    HeroMove = 5
}

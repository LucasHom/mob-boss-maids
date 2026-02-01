using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    // Singleton instance
    public static GameManager Instance { get; private set; }

    // Game state
    public bool isGameOver;

    //timer 
    public bool isTimerOver;

    // Events
    // Called when the game is over
    public event EventHandler OnGameOver;
    public event EventHandler InitializeSplurt;


    private void Awake()
    {
        // Singleton setup
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }


    private void Start()
    {
        OnGameOver?.Invoke(this, EventArgs.Empty);
        InitializeSplurt?.Invoke(this, EventArgs.Empty);
    }

    public void GameOver()
    {
        Debug.Log("hi");
    }
}
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    // Singleton instance
    public static GameManager Instance { get; private set; }

    // Game state
    public bool isGameOver;

    // Events
    // Called when the game is over
    public event Action OnGameOver;


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


    public void GameOver()
    {
        Debug.Log("hi");
    }
}
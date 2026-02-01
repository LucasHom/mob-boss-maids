using UnityEngine;
using System;
using System.Collections;

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
        StartCoroutine(WaitThenSplurt());
    }

    private IEnumerator WaitThenSplurt()
    {
        yield return null;
        InitializeSplurt?.Invoke(this, EventArgs.Empty);
    }

    public void GameOver()
    {
        Debug.Log("hi");
    }
}
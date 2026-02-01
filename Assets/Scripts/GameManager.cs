using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

public class GameManager : MonoBehaviour
{
    // Singleton instance
    public static GameManager Instance { get; private set; }

    // Game state
    public bool isGameOver;
    public bool isGameWin;

    //timer 
    public bool isTimerOver;
    
    // scene management
    public bool SceneLoadCondition = false;
    public string SceneName;

    // Events
    public event EventHandler OnGameOver;
    public event EventHandler OnGameWin;
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
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "MainScene")
        {
            StartCoroutine(WaitThenSplurt());
        }
    }

    private void Update()
    {
        // make sure to both set the condition as true, and name the specific scene.
        if (SceneLoadCondition)
        {
            SceneManager.LoadScene(SceneName);
        }

    }

    private IEnumerator WaitThenSplurt()
    {
        yield return null;
        InitializeSplurt?.Invoke(this, EventArgs.Empty);
    }

    public void GameOver()
    {
        if (isGameOver) return;

        isGameOver = true;
        OnGameOver?.Invoke(this, EventArgs.Empty);
        Debug.Log("game over!");
    }

    public void Win()
    {
        if (isGameWin) return;

        isGameWin = true;
        OnGameWin?.Invoke(this, EventArgs.Empty);
        Debug.Log("you win!");
        Debug.Log(SceneManager.GetActiveScene().name);
        if (SceneManager.GetActiveScene().name != "StartScene")
        {
            LoadNextScene();
        }

    }

    private void LoadNextScene()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentIndex + 1);
    }
}
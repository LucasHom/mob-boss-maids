using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Diagnostics;

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

    // audio
    public AudioClip clip;
    private AudioSource source;
    private bool playingSound = false;

    // Events
    public event EventHandler OnGameOver;
    public event EventHandler OnGameWin;
    public event EventHandler<InitializeSplurtEventArgs> InitializeSplurt;
    public class InitializeSplurtEventArgs : EventArgs
    {
        public Vector3 Position { get; private set; }
        public int Times { get; private set; }

        public InitializeSplurtEventArgs(Vector3 position, int times)
        {
            Position = position;
            Times = times;
        }
    }

    [Header("Audio")]
    [SerializeField] private AudioClip bgMusicClip;
    [SerializeField] private float bgMusicVolume = 0.5f;
    private AudioSource audioSource;

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

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = bgMusicClip;
        audioSource.loop = true;
        audioSource.volume = bgMusicVolume;
        audioSource.playOnAwake = false;
    }


    private void Start()
    {
        MakeSplurt.splurtCount = 0;

        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "MainScene")
        {
            StartCoroutine(WaitThenSplurt(new Vector3(0.825f, 2f, -1.4f), 5));
        }
        if (sceneName == "Kitchen")
        {
            StartCoroutine(WaitThenSplurt(new Vector3(0.9f, 0.9f, -2.5f), 7));
        }
        //if (sceneName == "MainScene")
        //{
        //    StartCoroutine(WaitThenSplurt(Vector3.zero, 3));
        //}

        if (source == null)
        {
            source = gameObject.AddComponent<AudioSource>();
        }

        PlayBackgroundMusic();
    }

    public void PlayBackgroundMusic()
    {
        if (!audioSource.isPlaying)
            audioSource.Play();
    }

    public void StopBackgroundMusic()
    {
        if (audioSource.isPlaying)
            audioSource.Stop();
    }

    private void Update()
    {
        // make sure to both set the condition as true, and name the specific scene.
        if (SceneLoadCondition)
        {
            SceneManager.LoadScene(SceneName);
        }

    }

    private IEnumerator WaitThenSplurt(Vector3 position, int times)
    {
        yield return null;
        InitializeSplurt?.Invoke(this, new InitializeSplurtEventArgs(position, times));
    }

    public void GameOver()
    {
        if (isGameOver) return;

        isGameOver = true;
        OnGameOver?.Invoke(this, EventArgs.Empty);
        // Debug.Log("game over!");

        if (clip != null && source != null && !playingSound)
        {
            source.PlayOneShot(clip);
            playingSound = true;
            StartCoroutine(WaitForSoundEnd());
        }
    }

    public void Win()
    {
        if (isGameWin) return;

        isGameWin = true;
        OnGameWin?.Invoke(this, EventArgs.Empty);
        // Debug.Log("you win!");
        // Debug.Log(SceneManager.GetActiveScene().name);
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

    private IEnumerator WaitForSoundEnd()
    {
        yield return new WaitForSeconds(5.0f);
        SceneManager.LoadScene("StartScene");
    }
}
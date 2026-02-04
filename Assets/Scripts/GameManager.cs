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

    //Game over
    private GameObject blackScreen;

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

    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene loaded: " + scene.name);
        isGameOver = false;



        blackScreen = GameObject.Find("blackScreen");

        if (blackScreen != null)
        {
            Debug.Log("BlackScreen found!");
        }
        else
        {
            Debug.LogWarning("No BlackScreen object found in scene!");
        }

        blackScreen.SetActive(false);


        // Equivalent of "Start" for each new scene
        if (scene.name == "MainScene")
        {
            StartCoroutine(WaitThenSplurt(new Vector3(0.825f, 2f, -1.4f), 5));
        }
        else if (scene.name == "Kitchen")
        {
            StartCoroutine(WaitThenSplurt(new Vector3(0.9f, 0.9f, -2.5f), 7));
        }

        PlayBackgroundMusic(); // optional: continue music if needed
        isGameWin = false;
    }

    private bool HasNextScene()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int totalScenes = SceneManager.sceneCountInBuildSettings;

        return currentIndex < totalScenes - 1;
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
        StartCoroutine(GameOverSequence());
    }

    private IEnumerator GameOverSequence()
    {
        audioSource.Pause();

        blackScreen.SetActive(true);
        SFXManager.Instance.PlaySFX("dead");
        yield return new WaitForSeconds(10f);
        audioSource.UnPause();
        SceneManager.LoadScene("StartScene");
    }

    public void Win()
    {
        if (isGameWin) return;

        isGameWin = true;
        OnGameWin?.Invoke(this, EventArgs.Empty);
        Debug.Log("you win!");
        Debug.Log(SceneManager.GetActiveScene().name);
        if (HasNextScene())
        {
            if (SceneManager.GetActiveScene().name != "StartScene")
            {
                LoadNextScene();
            }
        }
        else
        {
            StartCoroutine(EndingSequence());
        }
    }

    private IEnumerator EndingSequence()
    {
        audioSource.Pause();
        blackScreen.SetActive(true);
        SFXManager.Instance.PlaySFX("ending");
        yield return new WaitForSeconds(20f);
        audioSource.UnPause();
        SceneManager.LoadScene("StartScene");
    }

    private void LoadNextScene()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentIndex + 1);
    }
}
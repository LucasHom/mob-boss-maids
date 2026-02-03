using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class Page : MonoBehaviour
{
    [SerializeField] private Transform splurts;

    private bool gameStarting = false;
    private bool splurtsCleared = false;
    private bool grabbed = false;
    private int timesGrabbed = 0;

    private XRGrabInteractable grabInteractable;

    [SerializeField] private InputActionProperty aButton;

    void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(OnGrabbed);
        grabInteractable.selectExited.AddListener(OnReleased);
    }



    private void Start()
    {
        // Start the coroutine that checks every 1 second
        StartCoroutine(CheckChildrenRoutine());
        splurtsCleared = false;
        gameStarting = false;
        grabbed = false;
        timesGrabbed = 0;
    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {
        grabbed = true;
        timesGrabbed++; 
    }

    private void OnReleased(SelectExitEventArgs args)
    {
        grabbed = false;
    }

    private IEnumerator CheckChildrenRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            bool anySplurts = false;

            foreach (Transform child in splurts)
            {
                if (child.name.Contains("Splurt"))
                {
                    anySplurts = true;
                    break;
                }
            }

            if (!anySplurts)
            {
                Debug.Log("All Splurts cleared!");
                splurtsCleared = true;
                yield break; // stop the coroutine immediately
            }
        }
    }

    private void Update()
    {
        if (grabbed && !gameStarting && splurtsCleared)
        {
            gameStarting = true;
            StartCoroutine(StartGame());
        }

        if (timesGrabbed > 5)
        {
            SceneManager.LoadScene("MainScene");
        }

    }

    private IEnumerator StartGame()
    {
        SFXManager.Instance.PlaySFX("eyluigi");
        yield return new WaitForSeconds(15f);
        SceneManager.LoadScene("MainScene");
    }

    void OnDestroy()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrabbed);
        grabInteractable.selectExited.RemoveListener(OnReleased);
    }
}

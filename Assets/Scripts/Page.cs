using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Page : MonoBehaviour
{
    [SerializeField] private Transform splurts;

    private bool gameStarting = false;

    private void Start()
    {
        // Start the coroutine that checks every 1 second
        StartCoroutine(CheckChildrenRoutine());
    }

    private IEnumerator CheckChildrenRoutine()
    {
        while (!gameStarting)
        {
            yield return new WaitForSeconds(1f);
            // Check if splurts has 4 children
            Debug.Log("Checking children: " + splurts.childCount);
            if (splurts.childCount == 4)
            {
                gameStarting = true;
                // Start the coroutine that waits 5 seconds and then loads the scene
                StartCoroutine(StartGame());
            }
        }
    }

    private IEnumerator StartGame()
    {
        Debug.Log("Starting");
        yield return new WaitForSeconds(10f); // wait 5 seconds
        Debug.Log("Loading scene now");
        SceneManager.LoadScene("MainScene"); // load the scene AFTER wait
    }
}

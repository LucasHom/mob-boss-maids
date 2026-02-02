using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class TimeScript : MonoBehaviour
{
    public TMP_Text timeText;
    [SerializeField] private float totalSeconds;
    private float secondTimer;
    private bool isRunning;

    // public static float fiveMinuteLength = 3.125f; // 3.125 means that it's a five minute day

    // Start is called before the first frame update
    void Start()
    {
        //totalSeconds = 2 * 60.0f;
        secondTimer = 0.0f;
        isRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        // update both the time values
        if (isRunning)
        {
            secondTimer += Time.deltaTime;
        }
        if (secondTimer > 1.0f)
        {
            totalSeconds -= 1.0f;
            secondTimer = 0.0f;
        }

        // checking if the time reaches a certain value
        if (Mathf.Round(totalSeconds) == 0)
        {
            isRunning = false;
            GameManager.Instance.GameOver();
        }


        System.TimeSpan time = System.TimeSpan.FromSeconds(totalSeconds);

        // formatting for the UI display
        string formattedTime = string.Format("{00:D1}:{1:D2}",
            time.Minutes, time.Seconds);

        // Debug.Log(formattedTime);

        timeText.text = formattedTime;

        // // If it's in the last 10 minutes, have the timer flash red and white
        // if (totalSeconds % 60 >= 50)
        // {
        //     timeText.color = Color.red;
        // }
        // else
        // {
        //     timeText.color = Color.white;
        // }
    }
}
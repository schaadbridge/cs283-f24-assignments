using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    // total time in seconds
    public float totalLevelTime = 10f;
    public bool timerRunning = false;

    public TextMeshProUGUI timerText;
    // Start is called before the first frame update
    void Start()
    {
        timerRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (totalLevelTime > 0)
        {
            totalLevelTime -= Time.deltaTime;
        } else
        {
            Debug.Log("Out of time!");
            totalLevelTime = 0;
            timerRunning = false;
        }
        DisplayTime(totalLevelTime);
    }

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}

// all the needed methods from Unity will be imported 
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

// die Unterklasse Timer erbt alle Methoden der Oberklasse Monobehavior; Monobehavior ist für alle Unity-Scripts notwendig 
public class Timer : MonoBehaviour
{
    // creatinf private serialized fields
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private PlayerController playerController;
    // creating public variables and assigning them values
    public float timeRemaining = 180;
    public bool timerIsRunning = false;

    // creating a private method of type void
    private void Start()
    {
        // Starts the timer automatically
        timerIsRunning = true;
    }

    // creating a private method of type void
    void Update()
    {
        // if the remaining time is greater than zero...
        if (timeRemaining > 0)
        {
            // ...the remaining time substracts the interval from the last fraim to the current frame (Time.deltaTime)
            timeRemaining -= Time.deltaTime;
            // calling the method DisplayTime with the remaining time
            DisplayTime(timeRemaining);
        }
        else
        {
            // the remaining time is zero
            timeRemaining = 0;
            // the timer isn't running anymore
            timerIsRunning = false;
            // the method DeathSequence gets called
            playerController.DeathSequence();
        }
        
    }

    // creating a private Method of type IEnumerator to update the countdown
    private IEnumerator UpdateCountdown()
    {
        // suspending the execution of the function for a certain amount of time (one second)
        yield return new WaitForSeconds(1);
    }

    // creating a method of type void with the type float and the parameter "timeToDisplay"
    void DisplayTime(float timeToDisplay)
    {
        // adding the time to display by one
        timeToDisplay += 1;

        // calculating the minutes remaining 
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        // calculating the seconds remaining 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        // the text is shown in a string format
        // in the first {} the 0 stands for the variable "0" (minutes) and the "00" stands for a double-digit formating with 0 as placeholder
        // in the second {} the 1 stands for the variable "1" (seconds) and the "00" stands for a double-digit formating with 0 as placeholder
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
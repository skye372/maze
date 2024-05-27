// all the needed methods from Unity will be imported 
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

// die Unterklasse GameManager erbt alle Methoden der Oberklasse Monobehavior; Monobehavior ist für alle Unity-Scripts notwendig 
public class GameManager : MonoBehaviour
{
    // creating a public static field of type GameManager
    public static GameManager Instance { get; private set; }

    // creating a public array of gameobjects called players
    public GameObject[] player;

    // creating a private method of type void
    private void Awake()
    {
        // if the instance is not null...
        if (Instance != null)
        {
            // ...then the gameobject gets destroyed immediately
            DestroyImmediate(gameObject);
        }
        else
        {
            // the instant equals the class GameManager (this)
            Instance = this;
            // calling the method DontDestroyOnLoad on the targeted gameObject to not destroy it, while loading a new scene
            DontDestroyOnLoad(gameObject);
        }
    }

    // creating a public method of type void to check the winstate 
    public void CheckWinState()
    {
        // setting initial alive count to 0
        int aliveCount = 0;

        // creating a foreach loop to each player on the list of players (from the array)
        foreach (GameObject player in player)
        {
            // checking if the gameobject itself is active...
            if (player.activeSelf)
            {
                // ...then the alive count is incremented by one
                aliveCount++;
            }
        }

        // checking if the alive count is less or equal to one...
        if (aliveCount <= 1)
        {
            // ...then a new round starts within three seconds
            Invoke(nameof(NewRound), 3f);
        }
    }

    // creating public methods of type void
    public void GameOver()
    {
        // loading the GameOver scene
        SceneManager.LoadScene(0);
    }

    private void NewRound()
    {
        // reload the current scene and get the buildIndex
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void NextLevel()
    {
       // buildIndex has to be smaller than the largest buildIndex
       if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings-1) 
       {
            // loading the next scene in the buildIndex
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1); 
       } 
       else
       {
            // loading the first Level
            SceneManager.LoadScene(1); 
       }
    }
}

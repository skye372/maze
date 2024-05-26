using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject[] player;

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void CheckWinState()
    {
        int aliveCount = 0;

        foreach (GameObject player in player)
        {
            if (player.activeSelf)
            {
                aliveCount++;
            }
        }

        if (aliveCount <= 1)
        {
            Invoke(nameof(NewRound), 3f);
        }
    }

    public void GameOver()
    {
        SceneManager.LoadScene(0);
    }

    private void NewRound()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void NextLevel()
    {
       //buildIndex has to be smaller than the largest buildIndex
       if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings-1) 
       {
            //loading the next scene in the buildIndex
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1); 
       } 
       else
       {
            SceneManager.LoadScene(1); 
       }
    }
}

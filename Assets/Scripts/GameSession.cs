using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives=3;
    [SerializeField] int Score = 0;
    [SerializeField] TextMeshProUGUI LivesText;
    [SerializeField] TextMeshProUGUI ScoreText;
    private void Awake()
    {
        int numSessions = FindObjectsOfType<GameSession>().Length;
        if (numSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    private void Start()
    {
        LivesText.text = playerLives.ToString();
        ScoreText.text = Score.ToString();
    }
    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
        {
            TakeLives();
            
        }
        else
        {
            ResetGameSession();
        }
    }


    public void AddToScore(int pointToAdd)
    {
        Score+=pointToAdd;
        ScoreText.text = Score.ToString();
    }
    private void TakeLives()
    {
        playerLives --;
        LivesText.text = playerLives.ToString();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void ResetGameSession()
    {
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject Pause;
    private bool IsPaused;
    public GameObject GOobj;
    public static GameController Instace;
    public int totalScore;

    public int ScoreText;


    public int Score;

    void Awake()
    {
        Instace = this;
        
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        PauseGame();
    }

    public void UpdateScore( int value)
    {
        Score += value;
        //ScoreText.text = Score.ToString();
        
        PlayerPrefs.SetInt("Score", Score + totalScore);
    }

    public void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            IsPaused = !IsPaused;
            Pause.SetActive(IsPaused);
        }

        if (IsPaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void GameOver()
    {
        GOobj.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(1);
    }

    public void RestartMenu()
    {
        SceneManager.LoadScene(0);
    }
}

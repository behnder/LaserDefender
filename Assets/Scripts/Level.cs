using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    private float delayInSeconds = 2;
    [SerializeField] private GameObject score;
    private GameObject gameSession;

    private void Awake()
    {
        Screen.SetResolution(540, 960, false, 100);
        score = GameObject.Find("Score Text");
        gameSession = GameObject.Find("Game Session");

    }
    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoad());
        PrintScoreInScreen();
    }

    private void PrintScoreInScreen()
    {
        if (score != null)
        {
            score.GetComponent<TextMeshProUGUI>().text = gameSession.GetComponent<GameSession>().GetScore().ToString();
        }

    }

    private IEnumerator WaitAndLoad()
    {
        yield return new WaitForSeconds(delayInSeconds);
        SceneManager.LoadScene("Game Over");
    }

    public void LoadGameScene()
    {

    }
    public void LoadStartMenu()
    {
        gameSession.GetComponent<GameSession>().ResetScore();
        SceneManager.LoadScene(0);
    }
    public void LoadGame()
    {
        if (gameSession != null)
        {
            gameSession.GetComponent<GameSession>().ResetScore();
        }
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    void Update()
    {
        PrintScoreInScreen();
    }
}

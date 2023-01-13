using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    private int score = 0;
    GameObject scoreInScreen;


    private void Awake()
    {
       
        SetupSingleton();

    }

    private void SetupSingleton()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    public int GetScore()
    {
        return score;
    }


    public void AddScore(int scoreValue)
    {
        scoreInScreen = GameObject.Find("Score Text");
        score += scoreValue;
        scoreInScreen.GetComponent<TextMeshProUGUI>().text = score.ToString();
    }

    public void ResetScore()
    {
        Destroy(gameObject);
    }
}

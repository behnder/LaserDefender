using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    TextMeshProUGUI scoreText;
    GameSession gameSession;
    //GameObject gameSession;
    private void Awake()
    {
        gameSession = GameObject.Find("Game Session").GetComponent <GameSession>();

        scoreText = gameObject.GetComponent<TextMeshProUGUI>();
       // gameObject.GetComponent<TextMeshProUGUI>().text = gameSession.GetComponent<GameSession>().GetScore().ToString();
        scoreText.text = gameSession.GetScore().ToString();
    }
}

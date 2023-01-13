using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    private float health;
    TextMeshProUGUI healthText;
    Player player;
    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        healthText = gameObject.GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {
        healthText.text = player.GetHealth().ToString();
    }
}

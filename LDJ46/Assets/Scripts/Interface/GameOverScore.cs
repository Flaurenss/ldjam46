using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverScore : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI gameOverScoreText;

    void Start()
    {
        gameOverScoreText.text = "Score: " + PunctuationSystem.GetScore();
    }
}

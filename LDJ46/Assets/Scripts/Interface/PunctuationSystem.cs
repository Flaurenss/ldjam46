using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PunctuationSystem : MonoBehaviour
{
    [SerializeField] private EventsManager eventsManager;
    [SerializeField] private TextMeshProUGUI uiPunctuation;
    private static int _punctuation;

    private void Awake()
    {
        _punctuation = 0;
        ChangePunctuation(_punctuation);
        eventsManager.gameEventCompleted.AddListener(AddPoints);
    }

    private void AddPoints(float f = 0f)
    {
        _punctuation++;
        ChangePunctuation(_punctuation);
    }

    private void ChangePunctuation(int points)
    {
        uiPunctuation.text = points.ToString();
    }

    public static int GetScore() {
        return _punctuation;
    }
}

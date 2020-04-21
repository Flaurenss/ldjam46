using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PunctuationSystem : MonoBehaviour
{
    [SerializeField] private EventsManager eventsManager;
    [SerializeField] private TextMeshProUGUI uiPunctuation;
    [SerializeField] private int pointsToUnlockVampire;
    private static int _punctuation;

    public delegate void PointsUnlock();

    public static event PointsUnlock OnPointsReached;

    private void Awake()
    {
        _punctuation = 0;
        ChangePunctuation(_punctuation);
        eventsManager.gameEventCompleted.AddListener(AddPoints);
    }

    private void AddPoints(float f = 0f)
    {
        _punctuation++;
        CheckPunctuation();
        ChangePunctuation(_punctuation);
    }

    private void ChangePunctuation(int points)
    {
        uiPunctuation.text = points.ToString();
    }

    public static int GetScore() {
        return _punctuation;
    }

    public void CheckPunctuation()
    {
        if (_punctuation % pointsToUnlockVampire == 0)
        {
            OnPointsReached?.Invoke();
        }
    }
}

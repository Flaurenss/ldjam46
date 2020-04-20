using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PunctuationSystem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI uiPunctuation;
    private int _punctuation;

    private void Awake()
    {
        _punctuation = 0;
    }

    private void AddPoints()
    {
        _punctuation++;
        uiPunctuation.text = _punctuation.ToString();
    }
}

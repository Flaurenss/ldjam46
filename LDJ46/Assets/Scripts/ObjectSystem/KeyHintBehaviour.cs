using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyHintBehaviour : MonoBehaviour
{

    GameObject keyHintGameObject;
    Animator keyAnimator;

    void Awake()
    {
        var HintCanvas = transform.Find("HintCanvas");

        var KeyHint = HintCanvas.Find("KeyHint");

        keyHintGameObject = KeyHint.gameObject;

        keyAnimator = keyHintGameObject.GetComponent<Animator>();
        SetKeyHint(true);
    }
    
    public void SetKeyHint(bool state)
    {
        keyAnimator.SetBool("Hidden", !state);
    }

}

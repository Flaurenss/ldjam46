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
        Transform HintCanvas = transform.Find("HintCanvas");
        Transform KeyHint = null;

        if(HintCanvas != null)
            KeyHint = HintCanvas.Find("KeyHint");

        if (KeyHint != null)
            keyHintGameObject = KeyHint.gameObject;

        if (keyHintGameObject != null)
            keyAnimator = keyHintGameObject.GetComponent<Animator>();

        SetKeyHint(false);
    }
    
    public void SetKeyHint(bool state)
    {
        if (keyAnimator != null)
            keyAnimator.SetBool("Hidden", !state);
        else
            Debug.LogWarning("KeyAnimator is null (" + name + ")");
    }

}

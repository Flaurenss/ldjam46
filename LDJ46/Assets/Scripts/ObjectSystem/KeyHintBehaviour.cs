using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyHintBehaviour : MonoBehaviour
{

    GameObject keyHint;
    Animator keyAnimator;

    void Awake()
    {
        keyHint = transform.Find("HintCanvas").Find("KeyHint").gameObject;
        keyAnimator = keyHint.GetComponent<Animator>();
        SetKeyHint(true);
    }


    
    public void SetKeyHint(bool state)
    {
        keyAnimator.SetBool("Hidden", !state);
    }

}

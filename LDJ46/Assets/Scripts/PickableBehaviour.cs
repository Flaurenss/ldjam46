using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickableBehaviour : MonoBehaviour
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
        Debug.Log("Setting key hint: " + state);
        keyAnimator.SetBool("Hidden", !state);
       
    }

}

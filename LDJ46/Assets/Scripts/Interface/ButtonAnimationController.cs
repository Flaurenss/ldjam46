using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAnimationController : MonoBehaviour
{
    private Animator animator;
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void OnMouseEnter()
    {
        animator.SetBool("MouseOver", true);
    }

    void OnMouseExit()
    {
        animator.SetBool("MouseOver", false);
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float playerSpeed;

    public void MovePlayer(Vector3 movement)
    {
        if (movement == Vector3.zero) return;
        if (movement.magnitude > 1) movement = movement.normalized;
        var moveDir = movement * (playerSpeed * Time.deltaTime);
        transform.Translate(moveDir);
    }
}

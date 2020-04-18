using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float playerSpeed;

    public void MovePlayer(Vector3 movement)
    {
        var moveDir = movement.normalized * (playerSpeed * Time.deltaTime);
        transform.position += moveDir;
    }
}

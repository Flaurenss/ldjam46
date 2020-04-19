using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float playerSpeed;
    [SerializeField] private SpriteRenderer playerSprite;
    [SerializeField] private Sprite frontSprite;
    [SerializeField] private Sprite defaultSprite;

    public void MovePlayer(Vector3 movement)
    {
        if (movement == Vector3.zero) return;
        if (movement.magnitude > 1) movement = movement.normalized;
        CheckPlayerOrientation(movement);
        var moveDir = movement * (playerSpeed * Time.deltaTime);
        transform.Translate(moveDir);
    }

    private void CheckPlayerOrientation(Vector2 dir)
    {
        Debug.Log(dir);
        //TODO add sprite down change
        if (dir.y < 0 && dir.x == 0.0f)
        {
            playerSprite.sprite = frontSprite;
        }
        else
        {
            playerSprite.sprite = defaultSprite;
        }
        playerSprite.flipX = dir.x < 0;
    }
}

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
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private Sprite movementSprite;
    [SerializeField] private Sprite bloodSprite;
    [SerializeField] private Sprite pillsSprite;

    private Sprite _movementSprite;
    private Pocket _playerPocket;

    private void Awake()
    {
        _playerPocket = GetComponent<Pocket>();
    }

    public void MovePlayer(Vector3 movement)
    {
        // if (movement == Vector3.zero) return;
        if (movement.magnitude > 1) movement = movement.normalized;
        CheckSpriteType(movement);
        CheckPlayerOrientation(movement);
        var moveDir = movement * (playerSpeed * Time.deltaTime);
        transform.Translate(moveDir);
    }

    private void CheckPlayerOrientation(Vector2 dir)
    {
        if (dir.x < 0)
        {
            playerSprite.flipX = true;
        }
        else if (dir.x > 0)
        {
            playerSprite.flipX = false;
        }
    }

    private void CheckSpriteType(Vector2 movement)
    {
        if (_movementSprite == movementSprite && movement == Vector2.zero)
        {
            playerSprite.sprite = defaultSprite;
            return;
        }
        Debug.Log(movement);
        _movementSprite = _playerPocket.CurrentType == ItemsSingleton.ItemType.PILL ? pillsSprite :
            _playerPocket.CurrentType == ItemsSingleton.ItemType.BLOOD ? bloodSprite :
             movementSprite;
        playerSprite.sprite = _movementSprite;

    }
}

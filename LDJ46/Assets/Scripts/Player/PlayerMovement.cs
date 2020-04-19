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
        // if (_movementSprite == movementSprite && movement == Vector2.zero
        //  && _playerPocket.Empty)
        // {
        //     playerSprite.sprite = defaultSprite;
        //     return;
        // }
        // if (movement.y < 0 && _playerPocket.Empty)
        // {
        //     _movementSprite = defaultSprite;
        // }
        playerSprite.sprite = GetCarrySprite(movement);

    }

    private Sprite GetCarrySprite(Vector2 dir)
    {
        Debug.Log(dir);
        switch (_playerPocket.CurrentType)
        {
            case ItemsSingleton.ItemType.PILL:
                return pillsSprite;
                break;
            case ItemsSingleton.ItemType.BLOOD:
                return bloodSprite;
                break;
            case ItemsSingleton.ItemType.NONE:
                if (dir == Vector2.zero || dir.y < 0 && dir.x == 0 ) return defaultSprite;
                return movementSprite;
                    break;
            default:
                return movementSprite;
        }
    }
}

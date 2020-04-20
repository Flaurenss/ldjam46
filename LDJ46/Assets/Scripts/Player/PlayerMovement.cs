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

    private AudioSource audioSource;
    private Sprite _movementSprite;
    private Pocket _playerPocket;

    private void Awake()
    {
        _playerPocket = GetComponent<Pocket>();
        audioSource = GetComponent<AudioSource>();
    }

    public void MovePlayer(Vector3 movement)
    {
        if (movement.magnitude == 0)
        {
            audioSource.Stop();
        } 
        else if (!audioSource.isPlaying)
        {
            audioSource.loop = true;
            audioSource.Play();
        }

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
        playerSprite.sprite = GetCarrySprite(movement);
    }

    private Sprite GetCarrySprite(Vector2 dir)
    {
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

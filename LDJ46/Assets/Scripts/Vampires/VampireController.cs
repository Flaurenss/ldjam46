using System;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LorenzoResources.Scripts.Vampire
{
    public class VampireController : MonoBehaviour
    {
        [SerializeField] private float vampireSpeed;
        [SerializeField] private float freqToMove;
        private float _actualTime;
        private Vector2 _newDir;
        private Vector2 _previousDir;
        private RaycastHit2D[] _results;
        private Vector2[] rndDirections;
        private bool _newDirNeed = true;
        private bool _canMove = true;
        private bool _collisionPlayer = false;
        private Rigidbody2D _rb;
        private SpriteRenderer vampireSprite;


        private void Awake()
        {
            vampireSprite = GetComponent<SpriteRenderer>();
            _actualTime = 0f;
            _rb = GetComponent<Rigidbody2D>();
            rndDirections = new Vector2[]
            {
                Vector2.left,
                Vector2.up,
                Vector2.right,
                Vector2.down
            };
        }

        private void Update()
        {
            Debug.DrawRay(transform.position, _newDir *5, Color.magenta);
            if (_actualTime >= freqToMove && !_collisionPlayer)
            {
                _actualTime = 0;
                _canMove = !_canMove;
                _newDirNeed = true;
            }
            else
            {
                _actualTime += Time.deltaTime;
            }
        }

        private void FixedUpdate()
        {
            if (_newDirNeed)
            {
                _newDirNeed = false;
                _newDir = GetRandomDirection();   
            }

            if (!_canMove) return;
            transform.Translate(_newDir * (Time.deltaTime * vampireSpeed));
        }

        private Vector2 GetRandomDirection()
        {
            int rndDir = Random.Range(0, rndDirections.Length);
            _previousDir = _newDir;
            var newDir = rndDirections[rndDir];
            if (newDir == _previousDir)
            {
                GetRandomDirection();
            }
            FlipVampireSprite(newDir);
            return newDir;
        }

        private void FlipVampireSprite(Vector2 dir)
        {
            if (dir == Vector2.right) vampireSprite.flipX = false;
            if (dir == Vector2.left) vampireSprite.flipX = true;
        }
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.gameObject.CompareTag("Player"))
            {
                var old = _previousDir;
                var dirCollision = -other.contacts[0].normal;
                _newDirNeed = true;
            }
            else
            {
                _rb.constraints = RigidbodyConstraints2D.FreezeAll;
                _canMove = false;
                _collisionPlayer = true;
                _actualTime = freqToMove;
            }
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            if (!other.gameObject.CompareTag("Player"))
            {
                if (_newDir == -other.contacts[0].normal)
                {
                    _newDirNeed = true;
                }
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (!other.gameObject.CompareTag("Player")) return;
            _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            _canMove = true;
            _collisionPlayer = false;
        }
    }
}
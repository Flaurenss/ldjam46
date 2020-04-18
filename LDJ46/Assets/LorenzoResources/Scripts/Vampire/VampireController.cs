using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LorenzoResources.Scripts.Vampire
{
    public class VampireController : MonoBehaviour
    {
        [SerializeField] private float vampireSpeed;
        private Vector2 locationPos;
        private void Update()
        {
            
        }

        private void FixedUpdate()
        {
            var distance = locationPos - (Vector2)transform.position;
            if (distance.magnitude < 1f)
            {
                locationPos = GetRandomPosition();
            }
            // transform.Translate();
        }

        private Vector2 GetRandomPosition()
        {
            Vector2 rndDirection = Vector2.zero;
            var x = Random.Range(-1f,1f);
            var y = Random.Range(-1f,1f);
            rndDirection = new Vector2
            (
                Random.Range(-1f,1f),
                Random.Range(-1f,1f)
            );
            return Vector2.zero;
        }
    }
}
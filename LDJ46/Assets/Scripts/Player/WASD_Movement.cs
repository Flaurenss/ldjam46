using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WASD_Movement : MonoBehaviour
{
    public float speed;

    private Vector2 inputMovement;
    private Rigidbody2D rb2D;


    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        inputMovement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
    }

    void FixedUpdate()
    {
        Vector3 nextPos = transform.position + (Vector3) inputMovement * Time.fixedDeltaTime * speed;
        rb2D.MovePosition(nextPos);
    }
}

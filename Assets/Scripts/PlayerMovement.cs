using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 7f;
    private float movement;
    public float jumpHeight = 8f;

    private Rigidbody2D rb;
    private bool isGround;

    public Transform groundCheckPoint;
    public float groundCheckRadius = 0.2f;
    public LayerMask whatIsGround;

    public int jump_Count = 2;
    private int total_Jumps;

    private bool facing_Right;

    private void Start()
    {
        movement = 0f;
        isGround = true;
        facing_Right = true;
        rb = this.gameObject.GetComponent < Rigidbody2D>();
    }

    private void Update()
    {
        movement = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        Collider2D collInfo = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, whatIsGround);

        if (collInfo == true)
        {
            isGround = true;
        }
        else
        {
            isGround = false;
        }

        Flip();

    }

    private void FixedUpdate()
    {
        transform.position += new Vector3(movement, 0f, 0f) * Time.fixedDeltaTime * speed;
    }

    void Flip()
    {
        if (movement < 0f && facing_Right == true)
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
            facing_Right = false;
        }
        else if (movement > 0f && facing_Right == false)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            facing_Right = true;
        }
    }

    void Jump()
    {
        if (isGround == true)
        {
            rb.linearVelocityY = jumpHeight;
            isGround = false;
        }
        
    }

}

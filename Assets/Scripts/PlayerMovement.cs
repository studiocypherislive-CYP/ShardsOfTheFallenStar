using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    public Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 16f;
    private bool isFacingRight = true;

    [SerializeField] private Animator animator;

    // Animator parameter names
    private const string isRunningParam = "isRunning";
    private const string isJumpingParam = "isJumping";
    private const string isFallingParam = "isFalling";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // animator adding part
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
        
        UpdateAnimations();

        if (!isFacingRight && horizontal > 0f)
        {
            Flip();
        }
        else if (isFacingRight && horizontal < 0f)
        {
            Flip();
        }
    }

    // Updating animation parameters based on player state
    private void UpdateAnimations()
    {
        if (animator == null) return;

        bool isGrounded = IsGrounded();
        bool isMoving = Mathf.Abs(horizontal) > 0.1f;
        float verticalVelocity = rb.linearVelocity.y;

        // Set isRunning parameter - true when moving horizontally and grounded
        animator.SetBool(isRunningParam, isMoving && isGrounded);

        // Set isJumping parameter - true when moving upward (positive vertical velocity)
        animator.SetBool(isJumpingParam, !isGrounded && verticalVelocity > 0.1f);

        // Set isFalling parameter - true when moving downward (negative vertical velocity) and not grounded
        animator.SetBool(isFallingParam, !isGrounded && verticalVelocity < -0.1f);
    }

    //Jump system
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);
        }

        if (context.canceled && rb.linearVelocity.y > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
        }
    }

    //ground check system
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    //flip system
    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    //Move system
    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;

    }
    

}

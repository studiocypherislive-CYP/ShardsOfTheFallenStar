using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public int maxHealth = 3;
    private float movement;
    public float speed = 7f;
    private bool facing_Right;
    private Rigidbody2D rb;
    public float jumpHeight = 8f;
    private Animator animator;

    private bool isGround;
    private bool wasGrounded; // Track previous ground state

    public Transform groundCheckPoint;
    public float groundCheckRadius = 0.2f;
    public LayerMask whatIsGround;

    public int jump_Count = 2;
    private int total_Jumps;

    public Transform attackPoint;
    public float attackRadius = 1f;
    public LayerMask attackLayer;

    public Slider healthbar;
    private int collectedShards;
    public Text shardText;

    public Transform spawnPoint;
    public GameObject gameOverUI;

    private void Start()
    {
        movement = 0f;
        isGround = true;
        wasGrounded = true;
        facing_Right = true;
        total_Jumps = jump_Count;
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        animator = this.gameObject.GetComponent<Animator>();
        healthbar.maxValue = maxHealth;

        collectedShards = 0;
    }

    private void Update()
    {
        if (maxHealth <= 0)
        {
            Die();
        }

        shardText.text = collectedShards.ToString();
        healthbar.value = maxHealth;

        movement = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        Collider2D collInfo = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, whatIsGround);

        if (collInfo == true)
        {
            isGround = true;

            // Reset jumps and animation when landing
            if (!wasGrounded)
            {
                total_Jumps = jump_Count;
                animator.SetBool("Jump", false);
            }
        }
        else
        {
            isGround = false;
        }

        wasGrounded = isGround;

        Flip();

        if (Mathf.Abs(movement) > 0.1f)
        {
            animator.SetFloat("Run", 1f);
        }
        else if (movement < 0.1f)
        {
            animator.SetFloat("Run", 0f);
        }

        if (Input.GetMouseButtonDown(0))
        {
            PlayAttackAnimation();
            FindAnyObjectByType<AudioManager>().PlayAttackSound();

        }

    }

    private void PlayAttackAnimation()
    {
        int attack_Index = Random.Range(0, 3);

        if (attack_Index == 0)
        {
            animator.SetTrigger("Attack_1");
        }
        else if (attack_Index == 1)
        {
            animator.SetTrigger("Attack_2");
        }
        else
        {
            animator.SetTrigger("Attack_3");
        }
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
        if (total_Jumps > 0)
        {
            animator.SetBool("Jump", true);

            // Reset Y velocity to ensure consistent jump height
            Vector2 velocity = rb.linearVelocity;
            velocity.y = jumpHeight;
            rb.linearVelocity = velocity;

            total_Jumps -= 1;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Backup ground detection - checks if collision is with ground layer
        if (((1 << collision.gameObject.layer) & whatIsGround) != 0)
        {
            total_Jumps = jump_Count;
            animator.SetBool("Jump", false);
            isGround = true;
            wasGrounded = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Heart")
        {
            maxHealth += 1;
            collision.gameObject.GetComponent<Animator>().SetTrigger("HeartCollected");
            Destroy(collision.gameObject, .2f); //last argument is animation ending time/no need to put it
        }

        if (collision.gameObject.tag == "Shards")
        {
            collectedShards++; //same like maxHealth += 1;
            shardText.text = collectedShards.ToString();
            collision.gameObject.GetComponent<Animator>().SetTrigger("Shards_Collected");
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Traps")
        {
            TakeDamage();
        }
    }

    public void Attack()
    {
        Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, attackRadius, attackLayer);
        if (hit)
        {
            FindAnyObjectByType<EnemyController>().TakeDamage();
        }
    }

    public void TakeDamage()
    {
        if (maxHealth <= 0)
        {
            return;
        }
        maxHealth -= 1;
        animator.SetTrigger("Hurt");
    }

    private void Die()
    {
        //FindAnyObjectByType<AudioManager>().PlayExplosionSound();
        gameOverUI.SetActive(true);
        FindAnyObjectByType<GameManager>().isGameActive = false;
        Destroy(this.gameObject);
    }

    private void OnDrawGizmos()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }


}

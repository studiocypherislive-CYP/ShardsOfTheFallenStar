using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRadius = 1f;
    public LayerMask attackLayer;

    public float Speed = 2f;
    private bool facingRight;
    public Animator animator;
    public int maxHealth = 3;

    public Transform detectPoint;
    public float distance = 1f;
    public LayerMask targetLayer;
    public Transform player;
    public float attackRange = 5f;
    public float chaseSpeed = 3.5f;
    public float retrieveDistance = 2.5f;

    public GameObject explosionPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        facingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (maxHealth <= 0)
        {
            Die();
        }

        if (Vector2.Distance(transform.position, player.position) <= attackRange)
        {
            if (player.position.x < transform.position.x && facingRight == true)
            {
                transform.eulerAngles = new Vector3(0f, -180f, 0f);
                facingRight = false;
            }
            else if (player.position.x > transform.position.x && facingRight == false)
            {
                transform.eulerAngles = new Vector3(0f, 0f, 0f);
                facingRight = true;
            }

            if (Vector2.Distance(transform.position, player.position) > retrieveDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);
                animator.SetBool("Attack1", false);
            }
            else
            {
                animator.SetBool("Attack1", true);
            }
            
        }
        else
        {
            transform.Translate(new Vector2(1f, 0f) * Speed * Time.deltaTime);
        }


        RaycastHit2D hit = Physics2D.Raycast(detectPoint.position, Vector2.down, distance, targetLayer);

        if (hit == false)
        {
            if(facingRight == true)
            {
                transform.eulerAngles = new Vector3(0f, -180f, 0f);
                facingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0f, 0f, 0f);
                facingRight = true;
            }
        }
    }

    public void Attack()
    {
        Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, attackRadius, attackLayer);
        if (hit == true)
        {
            FindAnyObjectByType<PlayerMovement>().TakeDamage();
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

    private void OnDrawGizmos()
    {
        if (detectPoint == null)
        {
            return;
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(detectPoint.position, Vector2.down * distance);

        if (attackPoint == null)
        {
            return;
        }
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);

        //Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(transform.position, attackRange);

    }

    private void Die()
    {
        GameObject temp = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(temp, .91f);
        Destroy(this.gameObject);
    }



}

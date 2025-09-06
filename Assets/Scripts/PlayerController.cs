using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool jumped;
    public bool doubleJumped;

    public float jumpForce;
    public float liftingForce;

    private Rigidbody2D rb;
    private float timestamp;
    private BoxCollider2D boxCollider2D;

    public LayerMask whatIsGround;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        boxCollider2D = gameObject.GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (!GameManager.instance.isInGame)
        {
            return;
        }

        if (IsGrounded() && Time.time >= timestamp)
        {
            if (jumped || doubleJumped)
            {
                jumped = false;
                doubleJumped = false;
            }

            timestamp = Time.time + 1f;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (!jumped)
            {
                rb.linearVelocity = (new Vector2(0f, jumpForce));
                jumped = true;
            }
            else if (!doubleJumped)
            {
                rb.linearVelocity = (new Vector2(0f, jumpForce));
                doubleJumped = true;
            }
        }

        if (Input.GetMouseButton(0) && rb.linearVelocity.y < 0)
        {
            rb.AddForce(new Vector2(0f, liftingForce * Time.deltaTime));
        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, 0.1f, whatIsGround);

        return hit.collider != null;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle") && !GameManager.instance.immortality.isActive)
        {
            HandlePlayerDeath();
        }
        else if (collision.CompareTag("Coin"))
        {
            GameManager.instance.CoinCollected();
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Immortality"))
        {
            GameManager.instance.ImmortalityCollected();
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Magnet"))
        {
            GameManager.instance.MagnetCollected();
            Destroy(collision.gameObject);
        }
    }

    void HandlePlayerDeath()
    {
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        GameManager.instance.HandleGameOver();
    }
}

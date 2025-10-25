using UnityEngine;

public class BalloonController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 12f;

    Rigidbody2D rb;
    bool isGrounded;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float move = 0f;
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) move = -1f;
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) move = 1f;
        rb.linearVelocity = new Vector2(move * moveSpeed, rb.linearVelocity.y);

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Jump")) && isGrounded)
            Jump();
    }

    void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.collider.CompareTag("Platform"))
        {
            isGrounded = IsLandingOnTop(c);
            return;
        }

        if (c.collider.CompareTag("Obstacle"))
        {
            var shield = GetComponent<Shield>();
            if (shield && shield.IsActive)
            {
                shield.Consume();
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Max(rb.linearVelocity.y, jumpForce * 1.2f));

                var rend = c.collider.GetComponent<Renderer>(); if (rend) rend.enabled = false;
                var col = c.collider.GetComponent<Collider2D>(); if (col) col.enabled = false;
                return;
            }

            var gm = FindObjectOfType<BalloonGameManager>();
            if (gm) gm.HandleBalloonDeath();
            Destroy(gameObject);
        }
    }

    void OnCollisionStay2D(Collision2D c)
    {
        if (c.collider.CompareTag("Platform"))
            isGrounded = IsLandingOnTop(c);
    }

    void OnCollisionExit2D(Collision2D c)
    {
        if (c.collider.CompareTag("Platform"))
            isGrounded = false;
    }

    bool IsLandingOnTop(Collision2D c)
    {
        foreach (var cp in c.contacts)
            if (cp.normal.y > 0.5f) return true;
        return false;
    }
}

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Transform graphics;
    public ParticleSystem runParticles;
    public float moveSpeed = 10f;
    public float jumpForce = 10f;

    public BoxCollider2D groundCheckCollider;
    public LayerMask groundLayers;

    private Rigidbody2D rb2d;

    void Start()
    {
        if (!graphics) graphics = transform.GetChild(0);
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb2d.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, rb2d.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded())
            OnJump();

        if (rb2d.velocity.y <= 0 || Input.GetButtonUp("Jump"))
            rb2d.gravityScale = 2f;
        else if (rb2d.velocity.y >= 2f && rb2d.gravityScale < 2f)
            rb2d.gravityScale = 1f;
    }

    void OnJump()
    {
        rb2d.gravityScale = 0.2f;
        rb2d.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        LeanTween.cancel(graphics.gameObject);
        graphics.LeanScaleX(1.25f, 0.05f);
        graphics.LeanScaleY(0.5f, 0.05f).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() => graphics.LeanScale(Vector3.one, 0.1f).setEase(LeanTweenType.easeOutBack));


    }
    void OnLand(float xVel = 1)
    {
        LeanTween.cancel(graphics.gameObject);
        graphics.LeanScaleX(1 + xVel / 10, 0.05f);
        graphics.LeanScaleY(1 - xVel / 10, 0.05f).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() => graphics.LeanScale(Vector3.one, 0.1f).setEase(LeanTweenType.easeOutBack));

        if (runParticles)
            runParticles.Play();
    }
    void OnAir()
    {
        if (runParticles)
            runParticles.Stop();
    }



    bool IsGrounded()
    {
        if (!groundCheckCollider)
        {
            Debug.LogWarning("No GroundCheckCollider set!");
            return false;
        }

        return Physics2D.BoxCast(
                    transform.position,
                    groundCheckCollider.size,
                    0,
                    Vector2.down,
                    Vector2.Distance(transform.position, groundCheckCollider.transform.position),
                    groundLayers
                ); ;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsGrounded())
            OnLand(Mathf.Clamp(Mathf.Floor(collision.relativeVelocity.y), 0, 4));
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        OnAir();
    }
}

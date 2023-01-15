using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform graphics;
    [SerializeField] private SpriteRenderer graphicsRenderer;
    [SerializeField] private ParticleSystem runParticles;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private BoxCollider2D groundCheckCollider;
    [SerializeField] private LayerMask groundLayers;

    private Rigidbody2D rb2d;

    private bool lookR = true;

    void Start()
    {
        if (!graphics) graphics = transform.GetChild(0);
        if (!graphicsRenderer) graphicsRenderer = graphics.GetComponentInChildren<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        if (x < 0 && lookR)
            Flip();
        else if (x > 0 && !lookR)
            Flip();

        rb2d.velocity = new Vector2(x * moveSpeed, rb2d.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded())
            OnJump();

        if (rb2d.velocity.y <= 0 || Input.GetButtonUp("Jump"))
            rb2d.gravityScale = 2f;
        else if (rb2d.velocity.y >= 2f && rb2d.gravityScale < 2f)
            rb2d.gravityScale = 1f;
    }

    void Flip()
    {
        lookR = !lookR;
        if (graphicsRenderer)
            graphicsRenderer.flipX = !lookR;
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

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

    [HideInInspector] public bool lookR = true;
    private bool isGrounded = false;

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

        if (x != 0 && isGrounded)
            graphics.LeanRotateZ(Mathf.Sin(Mathf.Abs(moveSpeed * 2f) * Time.time * 2) * 5, 0.1f).setEase(LeanTweenType.easeOutBack);
        else graphics.LeanRotateZ(-x * 2, 0.1f).setEase(LeanTweenType.easeOutBack);

        if (Input.GetButtonDown("Jump") && CheckGround())
            OnJump();

        if (!isGrounded)
        {
            if (rb2d.velocity.y <= jumpForce * 0.2f || Input.GetButtonUp("Jump"))
                rb2d.gravityScale = 4f;
        }
        if (!isGrounded && CheckGround())
            OnLand(Mathf.Clamp(Mathf.Floor(Mathf.Abs(rb2d.velocity.y)), 1, 4));
        else if (isGrounded && !CheckGround()) OnAir();
    }

    void Flip()
    {
        lookR = !lookR;
        if (graphicsRenderer)
            graphicsRenderer.flipX = !lookR;
    }

    void OnJump()
    {
        rb2d.gravityScale = 1f;
        rb2d.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        LeanTween.cancel(graphics.gameObject);
        graphics.LeanScaleX(1.25f, 0.05f);
        graphics.LeanScaleY(0.5f, 0.05f).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() => graphics.LeanScale(Vector3.one, 0.1f).setEase(LeanTweenType.easeOutBack));


    }
    void OnLand(float xVel = 1)
    {
        isGrounded = true;
        LeanTween.cancel(graphics.gameObject);
        graphics.LeanScaleX(1 + xVel / 10, 0.05f);
        graphics.LeanScaleY(1 - xVel / 10, 0.05f).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() => graphics.LeanScale(Vector3.one, 0.1f).setEase(LeanTweenType.easeOutBack));

        if (runParticles)
            runParticles.Play();
    }
    void OnAir()
    {
        isGrounded = false;
        if (runParticles)
            runParticles.Stop();
    }

    bool CheckGround()
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
}

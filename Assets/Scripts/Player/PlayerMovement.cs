using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [Header("Movement parameters")]
    public float speed;
    public float jumpPower;
    [Header("Coyote Time")]
    public float coyoteTime;
    private  float coyoteCounter;
    [Header("Multiple Jumps")]
    public int extraJumps;
    private int jumpCounter;
    [Header("Wall Jumping")]
    public float wallJumpX;
    public float wallJumpY;
    [Header("Layers")]
    public LayerMask groundLayer;
    public LayerMask wallLayer;
    [Header("Sound")]
    public AudioClip jumpSound;
    private Rigidbody2D body;
    private Animator animator;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;
    private float horizontalInput;
    
    private void Awake()
    {
        // Grab references for rigidboy and animator from object
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        
        // Flip moving player
        if (horizontalInput > 0.01f)
        {
            transform.localScale = Vector3.one;
        }
        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        //Set animator parameters
        animator.SetBool("Run", horizontalInput != 0);
        animator.SetBool("grounded", isGrounded());
        //Wall jump logic
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        // Adjustable jump height
        if (Input.GetKeyUp(KeyCode.Space) && body.linearVelocity.y > 0)
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, body.linearVelocity.y / 2);
        }

        if (onWall())
        {
            body.linearVelocity = Vector2.zero;
            body.gravityScale = 0;
        }
        else
        {
            body.gravityScale = 5f;
            body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);
            if (isGrounded())
            {
                coyoteCounter = coyoteTime;
                jumpCounter = extraJumps;
            }
            else
            {
                coyoteCounter -= Time.deltaTime;
            }
        }
    }

    private void Jump()
    {
        if (coyoteCounter <= 0 && !onWall() && jumpCounter <= 0)
        {
            return;
        }
        SoundManager.instance.PlaySound(jumpSound);
        if (onWall())
        {
            wallJump();
        }
        else
        {
            if (isGrounded())
            {
                body.linearVelocity = new Vector2(body.linearVelocity.x, jumpPower);
            }
            else
            {
                if (coyoteCounter > 0)
                {
                    body.linearVelocity = new Vector2(body.linearVelocity.x, jumpPower);
                }
                else if (jumpCounter > 0)
                {
                    body.linearVelocity = new Vector2(body.linearVelocity.x, jumpPower);
                    jumpCounter--;
                }
                    coyoteCounter = 0;
            }
        }
    }

    private void wallJump()
    {
        body.AddForce(new Vector2(-Mathf.Sign(transform.localScale.x) * wallJumpX, wallJumpY));
        wallJumpCooldown = 0;
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    public bool canAtack()
    {
        return horizontalInput == 0 && isGrounded() && !onWall();
    }
}


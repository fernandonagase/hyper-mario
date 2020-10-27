using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioController : MonoBehaviour
{
    [SerializeField]
    private float speedMax = 5f;
    [SerializeField]
    private float jumpImpulse = 5f;
    [SerializeField]
    private float runFactor = 1.6f;
    private Rigidbody2D rb2d;
    private Animator animCtrl;
    private SpriteRenderer sprRenderer;
    private float horizontal = 0f;
    private bool isGrounded = false;
    private bool isRunning = false;
    private bool isCrouched = false;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animCtrl = GetComponent<Animator>();
        sprRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        CheckGround();
        horizontal = isCrouched ? 0 : Input.GetAxisRaw("Horizontal");

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb2d.AddForce(Vector2.up * jumpImpulse, ForceMode2D.Impulse);
        }

        isCrouched = Input.GetKey(KeyCode.LeftControl);

        UpdateAnimation();
    }

    void FixedUpdate()
    {
        if (!isCrouched)
        {
            float horizontalSpeed = horizontal * speedMax;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                horizontalSpeed *= runFactor;
                isRunning = true;
            }
            else
            {
                isRunning = false;
            }
            rb2d.velocity = new Vector2(horizontalSpeed, rb2d.velocity.y);

            // Mover responsabilidade de animação
            animCtrl.SetBool("isRunning", isRunning);
        }
    }


    void UpdateAnimation()
    {
        if (horizontal > 0f)
            sprRenderer.flipX = false;
        else if(horizontal < 0f)
            sprRenderer.flipX = true;

        animCtrl.SetFloat("speed", Mathf.Abs(horizontal));
        animCtrl.SetBool("isGrounded", isGrounded);
        animCtrl.SetBool("isCrouched", isCrouched);
    }

    void CheckGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f);
        isGrounded = hit.collider != null;
        Debug.DrawRay(transform.position, Vector2.down * 0.1f, Color.red);
    }
}

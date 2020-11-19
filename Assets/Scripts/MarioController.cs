using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioController : MonoBehaviour
{
    [SerializeField]
    private AudioSource _sfxSource;
    [SerializeField]
    private AudioClip _jumpAudio;

    [SerializeField]
    private float speedMax = 5f;
    private float jumpImpulse = 10f;
    [SerializeField]
    private float runFactor = 1.6f;
    [SerializeField]
    private VirtualButton _jumpButton = null;
    [SerializeField]
    private VirtualJoystick _joystick = null;
    private Rigidbody2D rb2d;
    private Animator animCtrl;
    private SpriteRenderer sprRenderer;
    private float horizontal = 0f;
    private float _deadzone = 0.15f;
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

        horizontal = Input.GetAxisRaw("Horizontal");
        if (horizontal != 0)
        {
            horizontal = isCrouched ? 0 : horizontal;
        }
        else
        {
            horizontal = isCrouched ? 0 : _joystick.GetHorizontal();
            horizontal = Mathf.Abs(horizontal) > _deadzone ? horizontal : 0.0f;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isGrounded) return;
            Jump();
        }
        else if (isGrounded && _jumpButton.DownEvent)
        {
            Jump();
        }

        isCrouched = Input.GetKey(KeyCode.S);

        UpdateAnimation();
    }

    void FixedUpdate()
    {
        if (!isCrouched)
        {
            float horizontalSpeed = horizontal * speedMax;
            if (Input.GetKey(KeyCode.LeftShift) && horizontalSpeed != 0)
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
        //if (horizontal > 0f)
        //    sprRenderer.flipX = false;
        //else if(horizontal < 0f)
        //    sprRenderer.flipX = true;

        if (horizontal != 0)
        {
            animCtrl.SetFloat("direction", horizontal);
        }
        animCtrl.SetFloat("speed", Mathf.Abs(horizontal));
        animCtrl.SetBool("isGrounded", isGrounded);
        animCtrl.SetBool("isCrouched", isCrouched);
    }

    void CheckGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position + Vector3.down * 0.1f,
            Vector2.down,
            0.1f
        );
        Collider2D otherCollider = hit.collider;

        isGrounded = otherCollider != null;
        Debug.DrawRay(transform.position, Vector2.down * 0.1f, Color.red);
    }









    public void Move(Vector2 velocity)
    {

    }

    public void Jump()
    {
        rb2d.AddForce(Vector2.up * jumpImpulse, ForceMode2D.Impulse);
        _sfxSource.PlayOneShot(_jumpAudio);
    }

    public void Crouch()
    {

    }
}

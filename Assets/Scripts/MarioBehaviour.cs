using UnityEngine;

public class MarioBehaviour : MonoBehaviour
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
    private float horizontal = 0f;
    private float _deadzone = 0.15f;
    private bool isGrounded = false;
    private bool _isCrouched = false;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animCtrl = GetComponent<Animator>();
    }

    void Update()
    {
        CheckGround();

        horizontal = Input.GetAxisRaw("Horizontal");
        if (horizontal != 0)
        {
            horizontal = _isCrouched ? 0 : horizontal;
        }
        else
        {
            horizontal = _isCrouched ? 0 : _joystick.GetHorizontal();
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

        Crouch(Input.GetKey(KeyCode.S));

        UpdateAnimation();
    }

    void FixedUpdate()
    {
        float horizontalSpeed = horizontal * speedMax;
        bool isRunning = horizontalSpeed != 0 && Input.GetKey(KeyCode.LeftShift);
        Move(Vector2.right * horizontalSpeed, isRunning);
    }


    void UpdateAnimation()
    {
        animCtrl.SetBool("isGrounded", isGrounded);
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









    public void Move(Vector3 velocity, bool isRunning)
    {
        animCtrl.SetFloat("speed", Mathf.Abs(velocity.x));

        if (velocity.x != 0)
        {
            animCtrl.SetFloat("direction", velocity.x);
        }

        if (isRunning)
        {
            velocity *= runFactor;
        }
        // Mover responsabilidade de animação
        animCtrl.SetBool("isRunning", isRunning);

        transform.position += velocity * Time.deltaTime;
    }

    public void Jump()
    {
        rb2d.AddForce(Vector2.up * jumpImpulse, ForceMode2D.Impulse);
        _sfxSource.PlayOneShot(_jumpAudio);
    }

    public void Crouch(bool isCrouched)
    {
        _isCrouched = isCrouched;
        animCtrl.SetBool("isCrouched", _isCrouched);
    }
}

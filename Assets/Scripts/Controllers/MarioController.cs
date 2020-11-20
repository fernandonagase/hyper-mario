using UnityEngine;

public class MarioController : CharacterController
{
    private const float WalkSpeed = 5f;
    private const float RunFactor = 1.6f;
    private const float JoystickDeadzone = 0.15f;

    private float _movement = 0.0f;
    private bool _isRunning = false;
    private bool _jumpEvent = false;

    [SerializeField]
    private VirtualButton _jumpButton = null;
    [SerializeField]
    private VirtualJoystick _joystick = null;
    private Animator animCtrl;

    // Start is called before the first frame update
    void Start()
    {
        movable = gameObject.GetComponent<MarioBehaviour>();
        animCtrl = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckGround();
        animCtrl.SetBool("isGrounded", ((MarioBehaviour)movable).IsGrounded);

        ((MarioBehaviour)movable).Crouch(Input.GetKey(KeyCode.S));

        if (!_jumpEvent)
        {
            _jumpEvent = Input.GetKeyDown(KeyCode.Space) || _jumpButton.DownEvent;
        }

        _movement = Input.GetAxisRaw("Horizontal");
        _movement = _movement != 0 ? _movement : _joystick.GetHorizontal();
        _movement = Mathf.Abs(_movement) > JoystickDeadzone ? _movement : 0.0f;
        _isRunning = _movement != 0 && Input.GetKey(KeyCode.LeftShift);
    }

    private void FixedUpdate()
    {
        if (_jumpEvent)
        {
            _jumpEvent = false;
            ((MarioBehaviour)movable).Jump();
        }

        float speed = _movement * WalkSpeed;
        if (_isRunning) speed *= RunFactor;
        movable.Move(Vector2.right * speed);
    }

    private void CheckGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position + Vector3.down * 0.1f,
            Vector2.down,
            0.1f
        );
        Collider2D otherCollider = hit.collider;

        ((MarioBehaviour)movable).IsGrounded = otherCollider != null;
    }
}

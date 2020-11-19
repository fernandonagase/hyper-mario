using UnityEngine;

public class MarioController : CharacterController
{
    private const float WalkSpeed = 5f;
    private const float RunFactor = 1.6f;
    private const float JoystickDeadzone = 0.15f;

    private float _movement;
    private bool _isRunning;

    [SerializeField]
    private VirtualButton _jumpButton = null;
    [SerializeField]
    private VirtualJoystick _joystick = null;

    // Start is called before the first frame update
    void Start()
    {
        movable = gameObject.GetComponent<MarioBehaviour>();        
    }

    // Update is called once per frame
    void Update()
    {
        ((MarioBehaviour)movable).Crouch(Input.GetKey(KeyCode.S));

        _movement = Input.GetAxisRaw("Horizontal");
        _movement = _movement != 0 ? _movement : _joystick.GetHorizontal();
        _movement = Mathf.Abs(_movement) > JoystickDeadzone ? _movement : 0.0f;
        _isRunning = _movement != 0 && Input.GetKey(KeyCode.LeftShift);
    }

    private void FixedUpdate()
    {
        float speed = _movement * WalkSpeed;
        if (_isRunning) speed *= RunFactor;
        movable.Move(Vector2.right * speed);
    }
}

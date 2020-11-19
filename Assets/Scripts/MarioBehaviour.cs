using UnityEngine;

public class MarioBehaviour : MonoBehaviour, IMovable
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
    private Rigidbody2D rb2d;
    private Animator animCtrl;
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isGrounded) return;
            Jump();
        }
        //else if (isGrounded && _jumpButton.DownEvent)
        //{
        //    Jump();
        //}

        UpdateAnimation();
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

    public void Move(Vector3 velocity)
    {
        animCtrl.SetFloat("speed", Mathf.Abs(velocity.x));
        // Mover responsabilidade de animação
        animCtrl.SetBool("isRunning", Mathf.Abs(velocity.x) > speedMax);

        if (_isCrouched) return;

        if (velocity.x != 0)
        {
            animCtrl.SetFloat("direction", velocity.x);
        }
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

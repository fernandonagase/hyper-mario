using UnityEngine;

public class MarioBehaviour : MonoBehaviour, IMovable
{
    [SerializeField]
    private AudioSource _sfxSource = null;
    [SerializeField]
    private AudioClip _jumpAudio = null;

    [SerializeField]
    private float speedMax = 5f;
    private float jumpImpulse = 10f;
    private Rigidbody2D rb2d;
    private Animator animCtrl;
    private bool _isCrouched = false;

    public bool IsGrounded { get; set; } = true;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animCtrl = GetComponent<Animator>();
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
        if (!IsGrounded) return;
        rb2d.AddForce(Vector2.up * jumpImpulse, ForceMode2D.Impulse);
        _sfxSource.PlayOneShot(_jumpAudio);
    }

    public void Crouch(bool isCrouched)
    {
        _isCrouched = isCrouched;
        animCtrl.SetBool("isCrouched", _isCrouched);
    }
}

using UnityEngine;

public class GoombaController : MonoBehaviour, ICyclicEnemy
{
    private float _speed = 3.0f;
    private int _direction = 1;
    private int _jumpFactor = 4;

    private Rigidbody2D _rb2d;
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        _rb2d.velocity = new Vector2(_speed * _direction, _rb2d.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D collider = collision.collider;
        if (collider.CompareTag("Player"))
        {
            Destroy(collider.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Waypoint"))
        {
            collision.GetComponent<IWaypoint>().ExecuteAction(GetComponent<Collider2D>());
        }
    }

    public void InvertMovement()
    {
        _direction = _direction * -1;
        _animator.SetFloat("direction", _direction);
    }

    public void Jump()
    {
        _animator.SetBool("isJumping", true);
        _rb2d.AddForce(Vector2.up * _jumpFactor, ForceMode2D.Impulse);
    }
}

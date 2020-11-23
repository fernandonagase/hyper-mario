using UnityEngine;

public class SmashColliderBehaviour : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.CompareTag("Enemy"))
        {
            IDamageable enemy = collision.GetComponent<IDamageable>();
            enemy?.TakeDamage();
        }
    }
}

using UnityEngine;

/* Dispara o pulo de um inimigo
 * 
 */
public class JumpWaypoint : MonoBehaviour, IWaypoint
{
    public void ExecuteAction(Collider2D collision)
    {
        // Remover chamada a implementação
        collision.GetComponent<GoombaBehaviour>().Jump();
    }
}

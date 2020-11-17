using UnityEngine;

/* Inverte o sentido de movimento de um objeto
 * 
 */
public class DirectionInverterWaypoint : MonoBehaviour, IWaypoint
{
    public void ExecuteAction(Collider2D collision)
    {
        ICyclicEnemy cyclicEnemy = collision
            .GetComponent<ICyclicEnemy>();
        if (cyclicEnemy != null)
        {
            cyclicEnemy.InvertMovement();
        }
    }
}

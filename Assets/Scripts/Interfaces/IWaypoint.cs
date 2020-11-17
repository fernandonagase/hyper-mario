using UnityEngine;

/* Interface que define o comportamento padrão a um waypoint
 * 
 * Permite que cada tipo de waypoint execute sua ação de modo
 * genérico
 * 
 */
public interface IWaypoint
{
    void ExecuteAction(Collider2D collision);
}
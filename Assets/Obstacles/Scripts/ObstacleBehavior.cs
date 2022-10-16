using UnityEngine;

public class ObstacleBehavior : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        PlayerHitPointsController damageble = other.collider.GetComponentInParent<PlayerHitPointsController>();
        if (damageble)
        {
            damageble.SetDamage(1);
        }
    }
}

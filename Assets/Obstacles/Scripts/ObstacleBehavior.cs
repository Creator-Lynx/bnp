using UnityEngine;

public class ObstacleBehavior : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        PlayerHitPointsController damageble = other.collider.GetComponentInParent<PlayerHitPointsController>();
        if (damageble)
        {
            Debug.Log(other.impulse.magnitude + "\n" + other.relativeVelocity.magnitude);
            if (other.relativeVelocity.sqrMagnitude > Mathf.Pow(damageble.impulseDamageTreashold, 2))
                damageble.SetDamage(1);
        }
    }
}

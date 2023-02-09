using UnityEngine;

public class EndLevelTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            BasicGameManager.CompleteLevel();
            other.GetComponentInParent<PlayerHitPointsController>()?.CompleteLevel();
        }
    }
}

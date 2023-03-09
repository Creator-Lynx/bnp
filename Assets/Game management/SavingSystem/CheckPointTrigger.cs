using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointTrigger : MonoBehaviour
{
    [SerializeField]
    bool isUsed = false;
    [SerializeField]
    ParticleSystem spikes, box;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isUsed)
        {
            SavingManager.MakeSave();
            isUsed = true;
            box.Play();
            spikes.TriggerSubEmitter(0);
            spikes.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
    }
}

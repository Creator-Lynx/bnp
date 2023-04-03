using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointTrigger : SelfSaver
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
            box.gameObject.SetActive(true);
            box.Play();
            spikes.TriggerSubEmitter(0);
            spikes.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
    }
    protected override void Save() { }
    protected override void Load()
    {
        Debug.Log("Triggered Checkpoint");
        box.gameObject.SetActive(true);
        box.Play();
    }
    public override void Awake()
    {
        base.Awake();
        Debug.Log("Awaked checkpoint");
    }

}

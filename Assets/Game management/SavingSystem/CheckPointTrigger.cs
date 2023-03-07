using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointTrigger : MonoBehaviour
{
    [SerializeField]
    bool isUsed = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isUsed)
        {
            SavingManager.MakeSave();
            isUsed = true;
        }
    }
}

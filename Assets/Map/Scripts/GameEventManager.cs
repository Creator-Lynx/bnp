using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventManager : MonoBehaviour
{
    [SerializeField]
    CameraCurveMover curveMover;
    [SerializeField]
    GameEvent[] GameEvents;
    int eventID = 0;
    void Update()
    {
        if (GameEvents.Length > 0 &&
        eventID < GameEvents.Length &&
        curveMover.t >= GameEvents[eventID].time)
        {
            MakeEvent();
        }
    }
    void MakeEvent()
    {
        GameEvents[eventID].OnCalledEvent.Invoke();
        eventID++;
    }
    public void TEST()
    {
        Debug.Log("Event is invoked");
    }
}

[System.Serializable]
public class GameEvent
{
    public float time;
    public UnityEvent OnCalledEvent;
}
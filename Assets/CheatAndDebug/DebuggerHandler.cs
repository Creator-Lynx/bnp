using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public class DebuggerHandler : MonoBehaviour
{
    public void StartDebug()
    {
        Profiler.logFile = "log";
        Profiler.enableBinaryLog = true;
        Profiler.enabled = true;
        Profiler.maxUsedMemory = 256 * 1024 * 1024;
    }

    public void EndDebug()
    {
        Profiler.enabled = false;
        Profiler.logFile = "";
    }
}

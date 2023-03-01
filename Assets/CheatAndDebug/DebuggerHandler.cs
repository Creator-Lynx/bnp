using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Profiling;

public class DebuggerHandler : MonoBehaviour
{
    public void StartDebug()
    {
        var docFolder =
        Environment.GetFolderPath(
            Environment.SpecialFolder.MyDocuments,
            Environment.SpecialFolderOption.Create);
        var path = Path.Combine(docFolder, "TinyBoat\\Logs");
        Directory.CreateDirectory(path);
        path = Path.Combine(path, "log_" + DateTime.Now.ToString("yyyy.MM.dd_g__HH-mm-ss") + ".raw");
        Debug.Log(path);
        Profiler.logFile = path;//"C:\\Users\\1_lin\\Documents\\log";
        Profiler.enableBinaryLog = true;
        Profiler.enabled = true;
    }
    public void SetVSync(int v)
    {
        QualitySettings.vSyncCount = v;
    }
    public void SetQueuedFrames(int q)
    {
        QualitySettings.maxQueuedFrames = q;
    }

    public void EndDebug()
    {
        Profiler.enabled = false;
        Profiler.logFile = "";
    }
    private void OnDisable()
    {
        EndDebug();
    }
}

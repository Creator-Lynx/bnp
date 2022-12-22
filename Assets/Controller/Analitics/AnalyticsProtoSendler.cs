using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Analytics;


public class AnalyticsProtoSendler : MonoBehaviour
{
    static AnalyticsProtoSendler instance;
    float startTime = 0;
    void Start()
    {
        startTime = Time.time;
        instance = this;
    }
    public static void SendAnalitics(int loadingSceneIndex, string loadingSceneName, int currentActiveSceneIndex, string currentActiveSceneName)
    {
        instance.SendStartedLevel(loadingSceneIndex, loadingSceneName);
        instance.SendTimeOnLevel(currentActiveSceneIndex, currentActiveSceneName);
    }

    void SendStartedLevel(int loadingSceneIndex, string loadingSceneName)
    {
        AnalyticsResult analyticsResult = Analytics.CustomEvent("StartedScene_r", new Dictionary<string, object>{
            {"SceneID", loadingSceneIndex},
            {"SceneName", loadingSceneName}
        });
        //Debug.Log("StartedLevel " + loadingSceneIndex + " " + analyticsResult);
    }

    void SendTimeOnLevel(int endedSceneIndex, string endedSceneName)
    {
        float time = Time.time - startTime;
        startTime = Time.time;
        AnalyticsResult analyticsResult = Analytics.CustomEvent("TimeOnLevel_r", new Dictionary<string, object>{
            {"SceneID", endedSceneIndex},
            {"SceneName", endedSceneName},
            {"Time", time}
        });
        //Debug.Log("EndedLevel " + endedSceneName + " " + analyticsResult + "\nTime " + time);
    }

    //for review send button
    public void SendReview(GameObject textArea)
    {
#if UNITY_EDITOR
#else
        TextMeshProUGUI[] txts = textArea.GetComponentsInChildren<TextMeshProUGUI>();
        TextMeshProUGUI text = txts[1],
        subText = txts[0];
        //Debug.Log("text " + text.name + "\nsubText " + subText.name);
        string message = text.text;
        AnalyticsResult analyticsResult = Analytics.CustomEvent("Review_r", new Dictionary<string, object>{
            {"Message", message}
        });
        textArea.GetComponentInParent<TMP_InputField>().text = string.Empty;
        //Debug.Log("review message = " + message);
        subText.text = analyticsResult.ToString();
        if (analyticsResult == AnalyticsResult.Ok) subText.text += "\nГотово, можешь отправлять еще, всё прочитаю";
#endif
    }

    //for button "blog" on review screen
    public void OpenURL()
    {
        Application.OpenURL("https://t.me/GameTrainProject");
    }
}

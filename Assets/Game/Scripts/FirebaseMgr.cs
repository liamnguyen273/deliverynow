using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Firebase;
using Firebase.Analytics;
using Firebase.Extensions;

public class FirebaseMgr : Singleton<FirebaseMgr>
{
    public static Action<Boolean> OnInited;
    bool mIsInited = false;
    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                InitializeFirebase(true);
            }
            else
            {
                InitializeFirebase(false);
                UnityEngine.Debug.LogError(System.String.Format("Could not resolve all Firebase dependencies: {0}", dependencyStatus));
            }
        });
    }

    void Update()
    {

    }

    void InitializeFirebase(bool result)
    {
        FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
        OnInited.Invoke(result);
        mIsInited = result;
    }

    public void LogEvent(string eventName, Dictionary<string, object> paramaters = null)
    {
        if (!mIsInited)
        {
            return;
        }

#if UNITY_EDITOR
        Debug.LogWarning("Tracking " + eventName);
#endif
        eventName = ValidateFirebaseName(eventName);
        if (paramaters == null || paramaters.Count == 0)
        {
            FirebaseAnalytics.LogEvent(eventName);
        }
        else
        {
            List<Parameter> list = new List<Parameter>();
            foreach (KeyValuePair<string, object> pair in paramaters)
            {
                if (pair.Value is long)
                {
                    list.Add(new Parameter(ValidateFirebaseName(pair.Key), (long)pair.Value));
                }
                else if (pair.Value is float || pair.Value is double)
                {
                    list.Add(new Parameter(ValidateFirebaseName(pair.Key), (double)pair.Value));
                }
                else
                {
                    list.Add(new Parameter(ValidateFirebaseName(pair.Key), pair.Value.ToString()));
                }
            }
            FirebaseAnalytics.LogEvent(eventName, list.ToArray());
        }
    }
    string ValidateFirebaseName(string inputStr)
    {
        string outputStr = inputStr.Replace(' ', '_');
        outputStr = outputStr.Replace('-', '_');
        return outputStr;
    }

}

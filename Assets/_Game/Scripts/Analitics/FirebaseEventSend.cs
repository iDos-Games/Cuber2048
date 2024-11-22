#if !UNITY_WEBGL && IDOSGAMES_FIREBASE_ANALYTICS
using Firebase.Analytics;
#endif
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirebaseEventSend : MonoBehaviour
{
    public void SendEvent(string parameterName)
    {
#if !UNITY_WEBGL && IDOSGAMES_FIREBASE_ANALYTICS
        FirebaseAnalytics.LogEvent(parameterName, parameterName, 1);
#endif
    }
}

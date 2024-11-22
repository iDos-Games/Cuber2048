#if !UNITY_WEBGL && IDOSGAMES_FIREBASE_ANALYTICS
using Firebase.Analytics;
#endif
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirebaseEventOnEnable : MonoBehaviour
{
    public string eventName;

    private void OnEnable()
    {
#if !UNITY_WEBGL && IDOSGAMES_FIREBASE_ANALYTICS
        FirebaseAnalytics.LogEvent(eventName, eventName, 1);
#endif
    }

}

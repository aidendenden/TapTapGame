using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private static TimeManager _instance;

    public static TimeManager Instance
    {
        get
        {
            if (_instance == null)
            {
                // Create a new GameObject if no instance exists
                GameObject obj = new GameObject("TimerManager");
                _instance = obj.AddComponent<TimeManager>();
                DontDestroyOnLoad(obj);
            }
            return _instance;
        }
    }

    // Static method to start the timer
    public static void StartTimer(System.Action callback, float duration)
    {
        Instance.StartCoroutine(Instance.TimerCoroutine(callback, duration));
    }

    private IEnumerator TimerCoroutine(System.Action callback, float duration)
    {
        yield return new WaitForSeconds(duration);
        callback.Invoke();
    }
}

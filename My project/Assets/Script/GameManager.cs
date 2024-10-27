using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            // if (_instance == null)
            // {
            //     _instance = FindObjectOfType<GameManager>();
            //
            //     if (_instance == null)
            //     {
            //         GameObject singleton = new GameObject(typeof(GameManager).Name);
            //         _instance = singleton.AddComponent<GameManager>();
            //         DontDestroyOnLoad(singleton);
            //     }
            // }
            return _instance;
        }
    }

    public Transform Hole;
    public Transform HoleOld;

    private void Start()
    {
        GameEventManager.OnTrigger += OnFinsh;
    }

    public void OnFinsh(string message,Transform _transform)
    {
        string t = "GameOnFinsh";
     if (string.Equals(message, t))
     {
         Hole.gameObject.SetActive(true);
         HoleOld.gameObject.SetActive(false);
         Debug.Log("Trigger event received: " + message, _transform);
     }
    }
    
}

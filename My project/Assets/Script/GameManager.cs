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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

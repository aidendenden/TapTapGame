using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassDetection : MonoBehaviour
{

    [SerializeField] PieceDetection[] pieceDetections;
    [SerializeField] GameObject watch;
    [SerializeField] GameObject puzzle;
    private void Start()
    {
       
        GameEventManager.Instance.AddListener(HandleTrigger);
    }

    private void OnDisable()
    {
      
        GameEventManager.Instance.RemoveListener(HandleTrigger);
    }
    private void HandleTrigger(string message, Transform _transform)
    {
        if (message == "PuzzlePass")
        {
            PuzzlePassDetect();
        }
    }
    void PuzzlePassDetect()
    {
        foreach (var pieceDetection in pieceDetections)
        {
            if (pieceDetection.IsMatch != true)
            {
                return;
            }
        }
        GameEventManager.Instance.Triggered("GameOnFinsh",transform);
        watch.SetActive(true);
      
    }
}

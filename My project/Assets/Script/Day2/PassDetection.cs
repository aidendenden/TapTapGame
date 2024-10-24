using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassDetection : MonoBehaviour
{

    [SerializeField] PieceDetection[] pieceDetections;
    private void Start()
    {
       
        GameEventManager.Instance.AddListener(HandleTrigger);
    }

    private void OnDisable()
    {
      
        GameEventManager.Instance.RemoveListener(HandleTrigger);
    }
    private void HandleTrigger(string message, Transform _transform, Vector3 _vector3)
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
        Debug.Log("isPass");
      
    }
}
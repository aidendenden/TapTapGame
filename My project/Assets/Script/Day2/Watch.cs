using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watch : MonoBehaviour
{
    public static bool isFinishPuzzle;
    [SerializeField] GameObject puzzle;
    public void showPuzzle()
    {
        if (!isFinishPuzzle)
        {
            puzzle.SetActive(true);
        }
    }
    public void Finish()
    {
        isFinishPuzzle = true;
    }
}

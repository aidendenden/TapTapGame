using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StoneAreaToUI : MonoBehaviour,IPointerClickHandler
{
    private CurSorManager cursorManager;
    [SerializeField] private bool isCursorIn = false;

    private void Start()
    {
        if (cursorManager==null)
        {
            cursorManager = FindObjectOfType<CurSorManager>(); // Find the CurSorManager in the scene
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isCursorIn)
        {
            cursorManager.isInStoneArea = true;
            isCursorIn = true;
        }
        else
        {
            cursorManager.isInStoneArea = false;
            isCursorIn = false;
        }
    }
}

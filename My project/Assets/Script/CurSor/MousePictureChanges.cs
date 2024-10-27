using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class MousePictureChanges : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler
{
    private CurSorManager cursorManager;
    [SerializeField] private bool isCursorIn = false;

    public bool stone;
    public bool isInteract;

    private void Start()
    {
        if (cursorManager==null)
        {
            cursorManager = FindObjectOfType<CurSorManager>(); // Find the CurSorManager in the scene
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!stone)
        {
            return;
        }
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
    
    public void OnPointerExit(PointerEventData eventData)
    {
        cursorManager.SetDefaultCursor();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isInteract)
        {
            cursorManager.SetInteractCursor();
        }
        else
        {
            cursorManager.SetInvestigateCursor();
        }
      
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractArea : MonoBehaviour
{
    private CurSorManager cursorManager;
    private Collider2D areaCollider;
    [SerializeField]private bool isCursorIn = false;
    private void Start()
    {
        cursorManager = FindObjectOfType<CurSorManager>();  // Find the CurSorManager in the scene
        areaCollider = GetComponent<Collider2D>(); // Get the collider of the investigate area
    }

    private void Update()
    {

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);


        if (areaCollider.OverlapPoint(mousePosition))
        {
            if (!isCursorIn)
            {
                cursorManager.SetInteractCursor();
                isCursorIn = true;
            }
           
        }
        else
        {
            if (isCursorIn)
            {
                cursorManager.SetDefaultCursor();
                isCursorIn = false;
            }             

        }
    }
}
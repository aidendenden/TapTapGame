using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvestigateArea : MonoBehaviour
{
    private CurSorManager cursorManager;
    private Collider2D areaCollider;
    [SerializeField] private bool isCursorIn = false;

    private void Start()
    {
        cursorManager = FindObjectOfType<CurSorManager>(); 
        areaCollider = GetComponent<Collider2D>();
    }

    private void Update()
    {

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);


        if (areaCollider.OverlapPoint(mousePosition))
        {
            if (!isCursorIn)
            {
                cursorManager.SetInvestigateCursor();
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

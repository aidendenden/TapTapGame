using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Paper : MonoBehaviour
{
    public Sprite burnPaper;
    private Collider2D areaCollider;
    [SerializeField] SpriteRenderer Renderer;
    [SerializeField] private bool isCursorIn = false;
    private void Start()
    {      
        areaCollider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (areaCollider.OverlapPoint(mousePosition))
        {

            UIDrag.isInPaperArea = true;
            if (!isCursorIn)
            {
                
                isCursorIn = true;
            }
        }
        else
        {
            UIDrag.isInPaperArea = false;
            if (isCursorIn)
            {
               
                isCursorIn = false;
            }
        }
    }
    public void BurnPaper()
    {
        Renderer.sprite = burnPaper;
    }
}

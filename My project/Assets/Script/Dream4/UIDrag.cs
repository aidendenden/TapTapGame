using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIDrag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField]private RectTransform rectTransform;
    [SerializeField]private Canvas canvas;
    private Vector3 originPoint;
    private Paper paper;
    private Inventory inventory;
    public static bool isInPaperArea;
    [SerializeField]private Slot slot;
    private void Awake()
    {
        slot = GetComponentInParent<Slot>();
        rectTransform = GetComponent<RectTransform>();
        canvas = FindObjectOfType<Canvas>();
        originPoint = transform.position;
        paper=FindObjectOfType<Paper>();
        inventory = FindObjectOfType<Inventory>();
        
    }
    private void Start()
    {
        slot = GetComponentInParent<Slot>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        
    }

    public void OnDrag(PointerEventData eventData)
    {
       
        if (canvas == null) return;
      
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = originPoint;
        if (isInPaperArea)
        {
            paper.BurnPaper();
            inventory.RemoveItem(slot);
        }
    }
   
}

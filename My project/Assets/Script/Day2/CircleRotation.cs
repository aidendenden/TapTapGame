using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleRotation : MonoBehaviour
{
    [SerializeField]private float rotationDuration;
    public LayerMask targetLayer;
    private BoxCollider2D boxCollider;

    void Start()
    {       
        boxCollider = GetComponent<BoxCollider2D>();
    }
    void OnMouseDown()
    {       
        Collider2D[] collidersWithin = Physics2D.OverlapBoxAll(boxCollider.bounds.center, boxCollider.bounds.size, 0f, targetLayer);
       
        foreach (Collider2D collider in collidersWithin)
        {
            collider.transform.SetParent(transform);
        }
        transform.DORotate(new Vector3(0f, 0f, transform.rotation.eulerAngles.z - 120f), rotationDuration, RotateMode.Fast);
        TimeManager.StartTimer(()=> GameEventManager.Instance.Triggered("PuzzlePass", transform, Vector3.zero),0.25f);       
    }
}       

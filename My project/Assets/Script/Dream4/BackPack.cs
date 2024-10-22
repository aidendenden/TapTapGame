using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackPack : MonoBehaviour
{
    public bool isShow;
    public float v;
    public void ShowAndHideBackPack()
    {

        if (isShow)
        {
            isShow = false;
            transform.DOMoveX(transform.position.x - v, 0.5f);
        }else 
        {
            isShow = true;
            transform.DOMoveX(transform.position.x + v, 0.5f);
        }
    
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemGet : MonoBehaviour
{
    private bool IsItemGet;
    public Button button;
    public void GetItem()
    {
        Debug.Log("���һ����");
        button.interactable = false;
    } 
}

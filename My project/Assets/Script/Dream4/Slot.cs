using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public bool isOccupied;
    public Item Item;

    public void Remove()
    {
        if (!isOccupied) { return; }
        isOccupied = false;
        Destroy(Item.gameObject);
    }
}

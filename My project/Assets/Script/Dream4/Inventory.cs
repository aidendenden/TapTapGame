using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public List<Item> items;
    public List<Slot> slots;

    private void Start()
    {
        items = new List<Item>();
    }
    public void AddItem(Item item)
    {      
        foreach (var slot in slots)
        {
            if (slot.isOccupied==false)
            {
                slot.isOccupied = true;
                Item _item =Instantiate(item, slot.gameObject.transform.position,Quaternion.identity);
                _item.transform.SetParent(slot.gameObject.transform);
                slot.Item = _item;
                break;
            }
        }
    }
    public void RemoveItem(Slot slot)
    {
        slot.Remove();
        slot.transform.SetAsLastSibling();
        int index =slots.IndexOf(slot);
        MoveToLast<Slot>(slots,index);
    }
    
    public void MoveToLast<T>(List<T> list, int index)
    {
        if (index >= 0 && index < list.Count)
        {
            T element = list[index];  
            list.RemoveAt(index);     
            list.Add(element);        
        }
    }
}

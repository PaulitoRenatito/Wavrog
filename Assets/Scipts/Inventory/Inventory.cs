using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    
    public event EventHandler OnItemAdded;

    [SerializeField] private List<ItemSO> items;

    public void AddItem(ItemSO itemSO)
    {
        items.Add(itemSO);
        OnItemAdded?.Invoke(this, EventArgs.Empty);
    }

    public List<ItemSO> GetItems()
    {
        return items;
    }
    
}

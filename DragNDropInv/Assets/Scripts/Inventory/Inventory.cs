using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Image slotPrefab;
    private readonly List<ICollectable> collectedObjects = new List<ICollectable>();

    public event EventHandler<InventoryEventArgs> ItemAdded; 
    public event EventHandler<InventoryEventArgs> ItemRemoved; 

    public void AddItem(ICollectable item)
    {
        Image slot = Instantiate(slotPrefab);
        slot.transform.SetParent(this.transform, false);
        collectedObjects.Add(item);
        item.Collect();

        if (ItemAdded != null)
        {
            ItemAdded(this, new InventoryEventArgs(item));
        }
    }

    public void RemoveItem(ICollectable item)
    {
        if (collectedObjects.Contains(item))
        {
            collectedObjects.Remove(item);
            item.Drop();

            if (ItemRemoved != null)
            {
                ItemRemoved(this, new InventoryEventArgs(item));
            }
        }
    }
}

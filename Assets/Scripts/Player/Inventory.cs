using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Inventory : NetworkBehaviour {
    /// <summary>
    /// Item id's that are in the inventory
    /// </summary
    private SyncListInt items = new SyncListInt();
    public int size;
    [SyncVar]
    int currentItem;
    [SyncVar]
    private int numItems;

    public override void OnStartServer()
    {
        for(int i = 0; i < size; i++)
        {
            items.Add(-1);
        }
        currentItem = 0;
    }

    public SyncListInt GetItems()
    {
        return items;
    }

    public void RemoveItem(int index)
    {
        if (items[index] != -1)
        {
            items[index] = -1;
            numItems--;
        }
    }

    /// <summary>
    /// The currently "held" item by the player
    /// </summary>
    /// <returns>The item, or null if none selected</returns>
    public int GetCurrentItem()
    {
        return items[currentItem];
    }

    public void RemoveCurrentItem()
    {
        RemoveItem(currentItem);
    }

    /// <summary>
    /// Adds the item to the inventory if not full
    /// </summary>
    /// <param name="item">Item to add</param>
    /// <returns>true if added. false if inventory is full</returns>
    public bool AddItem(int item)
    {

        if (IsFull())
        {
            return false;
        }
        for (int i = 0; i < size; i++)
        {
            if (items[i] == -1)
            {
                items[i] = item;
                break;
            }
        }
        numItems++;
        return true;
    }


    public bool IsFull()
    {
        return numItems >= size;
    }
}

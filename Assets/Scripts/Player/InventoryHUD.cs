using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class InventoryHUD : MonoBehaviour {

    public GameObject player;
    private Inventory inventory;
    public Color defaultColor, selectedColor;
    public GameObject[] slotObjects = new GameObject[Inventory.SIZE];
    private InventorySlot[] slots = new InventorySlot[Inventory.SIZE];

    void Start () {
        inventory = player.GetComponent<Inventory>();
        for(int i = 0; i < slotObjects.Length; i++)
        {
            slots[i] = slotObjects[i].GetComponent<InventorySlot>();
            if(slots[i] == null)
            {
                throw new System.Exception("Slot does not have InventorySlot script");
            }
        }
	}
	
	void Update () {
        SyncListInt items = inventory.GetItems();
        for(int i = 0; i < slots.Length; i++)
        {
            if (items[i] == -1)
            {
                slots[i].itemImage.enabled = false;
            }
            else
            {
                slots[i].itemImage.enabled = true;
                slots[i].itemImage.sprite = Game.ITEM_SPRITES[items[i]];
            }
            if(inventory.GetCurrentItemIndex() == i)
            {
                slots[i].outlineImage.color = selectedColor;
            } else
            {
                slots[i].outlineImage.color = defaultColor;
            }
        }
    }
}

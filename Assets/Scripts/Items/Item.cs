using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Collider))]
public class Item : NetworkBehaviour {

    /// <summary>
    /// The unique identifier for this item type. Determined by index in ItemGenerator
    /// </summary>
    [SerializeField]
    protected int id;

    /// <summary>
    /// The player using the item
    /// </summary>
    protected GameObject user;

    public static List<Item> allItems = new List<Item>();

	void Start () {
	}
	
	void Update () {
		//TODO Sinosoidal motion
	}

    /// <summary>
    /// Uses an "item" that does not currently exist.
    /// </summary>
    /// <param name="id">The item type</param>
    /// <param name="user">The player that is using the item</param>
    [Server]
    public static void UseItemFromInventory(int id, GameObject user)
    {
        Item itemPrefab = allItems[id];
        GameObject itemObject = Instantiate(itemPrefab.gameObject, user.transform.position, Quaternion.identity);
        Item item = itemObject.GetComponent<Item>();
        item.user = user;
        item.UseItem();
    }

    [Server]
    public virtual void UseItem()
    {
        user.GetComponent<Inventory>().RemoveCurrentItem();
    }

    [Server]
    protected void SpawnItem()
    {
        NetworkServer.Spawn(this.gameObject);
    }

    private void TryPickUp(GameObject other)
    {
        
        if (user != null)
        {
            return;
        }
            Inventory inventory = other.GetComponent<Inventory>();
        if (inventory == null)
        {
            //Not player
            return;
        }
        if (inventory.AddItem(this.id))
        {
            
            //Picked up
            NetworkServer.Destroy(this.gameObject);
        }
    }

    [ServerCallback]
    protected void OnTriggerEnter(Collider other)
    {
        TryPickUp(other.gameObject);
    }
    
    [ServerCallback]
    protected void OnColliderEnter(Collision other)
    {
        TryPickUp(other.gameObject);
    }

    public void SetId(int i)
    {
        
        id = i;
    }
}

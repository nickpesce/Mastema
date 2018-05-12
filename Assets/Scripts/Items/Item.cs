using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Collider))]
public class Item : NetworkBehaviour {

    /// Get set by 'Game' object on start
    public static List<Item> allItems = new List<Item>();
    public static List<Sprite> allItemSprites = new List<Sprite>();
    /// <summary>
    /// The unique identifier for this item type. Determined by index in Game
    /// </summary>
    [SerializeField]
    protected int id;

    [SerializeField]
    protected int weight;

    /// <summary>
    /// The player using the item
    /// </summary>
    protected GameObject user;

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
    public static void UseItemFromInventory(int id, GameObject user, Vector3 position, Vector3 direction)
    {
        Item itemPrefab = allItems[id];
        GameObject itemObject = Instantiate(itemPrefab.gameObject, user.transform.position, Quaternion.identity);
        Item item = itemObject.GetComponent<Item>();
        item.user = user;
        item.UseItem(position, direction);
    }

    [Server]
    public static void SpawnOnGround(int id, Vector3 position)
    {
        GameObject item = Instantiate(allItems[id].gameObject, position, Quaternion.identity);
        NetworkServer.Spawn(item);
    }


    [Server]
    public virtual void UseItem(Vector3 position, Vector3 direction)
    {
        user.GetComponent<Inventory>().RemoveCurrentItem();
    }

    [Server]
    protected void SpawnItem()
    {
        NetworkServer.Spawn(this.gameObject);
    }

    [Server]
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

    public void SetWeight(int w)
    {

        weight = w;
    }

    public int GetWeight()
    {
        return weight;
    }
}

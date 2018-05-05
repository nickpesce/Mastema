using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(FloorController))]
public class ItemGenerator : NetworkBehaviour {

    public float spawnChancePerUpdate = .01f;
    FloorController floorController;
    public Item[] items;

    void Start()
    {
        floorController = GetComponent<FloorController>();
        for(int i = 0; i < items.Length; i++)
        {
            items[i].SetId(i);
            Item.allItems.Insert(i, items[i]);
        }
    }

    [ServerCallback]
    void FixedUpdate()
    {
        if (Random.value < spawnChancePerUpdate)
        {
            Vector3 spawnLocation = floorController.GetRandomPosition();
            GameObject item = Instantiate(GetRandomItem().gameObject, spawnLocation, Quaternion.identity);
            NetworkServer.Spawn(item);
        }
    }

    [Server]
    Item GetRandomItem()
    {
        return items[Random.Range(0, items.Length)];
    }

    public Item[] GetItemList()
    {
        return items;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(FloorController))]
public class ItemGenerator : NetworkBehaviour {

    public float spawnChancePerUpdate = .01f;
    FloorController floorController;

    void Start()
    {
        floorController = GetComponent<FloorController>();

    }

    [ServerCallback]
    void FixedUpdate()
    {
        if (Random.value < spawnChancePerUpdate)
        {
            Vector3 spawnLocation = floorController.GetRandomPosition();
            Item.SpawnOnGround(Random.Range(0, Item.allItems.Count), spawnLocation);
        }
    }

    [Server]
    Item GetRandomItem()
    {
        return Item.allItems[Random.Range(0, Item.allItems.Count)];
    }
}

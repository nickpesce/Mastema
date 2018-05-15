using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(FloorController))]
public class ItemGenerator : NetworkBehaviour {

    public float spawnChancePerUpdate = .01f;
    FloorController floorController;

    private int totalWeight = 0;
    private int[] weights;


    void Start()
    {
        floorController = GetComponent<FloorController>();

        weights = new int[Item.allItems.Count];

        for (int i = 0; i < Item.allItems.Count; i++)
        {
            totalWeight += Item.allItems[i].GetWeight();
        }

        weights[0] = Item.allItems[0].GetWeight();
        for (int i = 1; i < Item.allItems.Count; i++)
        {
            weights[i] = weights[i - 1] + Item.allItems[i].GetWeight();
        }
    }

    [ServerCallback]
    void FixedUpdate()
    {
        
        if (Random.value < spawnChancePerUpdate)
        {
            int pickWeight = Random.Range(0, totalWeight);
            int itemToGen = 0;
            for (int i = 0; i < weights.Length; i++)
            {
                if (pickWeight < weights[i])
                {
                    itemToGen = i;
                    break;
                }
            }

           
            Vector3 spawnLocation = floorController.GetRandomPosition();
            Item.SpawnOnGround(itemToGen, spawnLocation);
        }
    }

    [Server]
    Item GetRandomItem()
    {
        return Item.allItems[Random.Range(0, Item.allItems.Count)];
    }
}

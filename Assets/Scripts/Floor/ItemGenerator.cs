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

        weights = new int[Game.ITEMS.Count];

        for (int i = 0; i < Game.ITEMS.Count; i++)
        {
            totalWeight += Game.ITEMS[i].GetWeight();
        }

        weights[0] = Game.ITEMS[0].GetWeight();
        for (int i = 1; i < Game.ITEMS.Count; i++)
        {
            weights[i] = weights[i - 1] + Game.ITEMS[i].GetWeight();
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
        return Game.ITEMS[Random.Range(0, Game.ITEMS.Count)];
    }
}

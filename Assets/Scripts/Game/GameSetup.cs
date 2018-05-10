using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetup : MonoBehaviour {

    public Item[] items;
    public Sprite[] itemSprites;

	// Use this for initialization
	void Start () {
        for(int i = 0; i<items.Length; i++)
        {
            items[i].SetId(i);
            Item.allItems.Insert(i, items[i]);
        }
        for (int i = 0; i < items.Length; i++)
        {

            Item.allItemSprites.Insert(i, itemSprites[i]);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

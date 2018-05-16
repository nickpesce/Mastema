using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

    //Inputs
    public Item[] items;
    public int[] itemWeights;
    public Sprite[] itemSprites;

    //Statics
    public static List<Item> ITEMS = new List<Item>();
    public static List<Sprite> ITEM_SPRITES = new List<Sprite>();
    public static string PLAYER_NAME = "Steve";

	// Use this for initialization
	void Start () {
        for(int i = 0; i<items.Length; i++)
        {
            items[i].SetId(i);
            items[i].SetWeight(itemWeights[i]);
            ITEMS.Insert(i, items[i]);

}
        for (int i = 0; i < items.Length; i++)
        {
            ITEM_SPRITES.Insert(i, itemSprites[i]);
            Game.ITEM_SPRITES.Insert(i, itemSprites[i]);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

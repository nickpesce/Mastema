using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorGenerator : MonoBehaviour {
    public int halfWidth, halfHeight;
    public float tileSize;
    public GameObject tilePrefab;
	// Use this for initialization
	void Start () {
        tilePrefab.transform.localScale = new Vector3(tileSize, tileSize, tileSize);
        for (int r = -halfWidth; r < halfWidth; r++)
        {
            for(int c = -halfHeight; c < halfHeight; c++)
            {
                GameObject tileClone = Instantiate(tilePrefab, transform.position + new Vector3(r*tileSize, 0, c*tileSize), Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}

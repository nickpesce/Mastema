using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorController : MonoBehaviour
{
    public int width, height;
    public float tileSize;
    public GameObject tilePrefab;
    private GameObject[][] tiles;
    // The floor object is the center of the floor, but the tiles matrix is relative to the bottom left corner.
    private Vector3 bottomLeftCorner;

    void Start()
    {
        tiles = new GameObject[height][];
        bottomLeftCorner = transform.position - new Vector3(height/2f*tileSize, 0, width/2f*tileSize);
        generateFloor();
    }

    void generateFloor()
    {
        for (int r = 0; r < height; r++)
        {
            tiles[r] = new GameObject[width];
            for (int c = 0; c < width; c++)
            {
                Vector3 offset = new Vector3(r * tileSize, 0,c * tileSize);
                GameObject tile = Instantiate(tilePrefab, bottomLeftCorner + offset, Quaternion.identity);
                tile.transform.localScale *= tileSize;
                tiles[r][c] = tile;
            }
        }
    }

    //float lastDestroy = 0;
    void Update()
    {
        /*
        if(Time.time - lastDestroy > 1)
        {
            destroyTile(transform.position + new Vector3(((Random.value * height) - (height / 2f)) * tileSize, 0, ((Random.value * width) - (width / 2f)) * tileSize));
            lastDestroy = Time.time;
        }
        */
    }

    void FixedUpdate()
    {
    }

    // Position is in world coordinates. Coord is in tile matrix.
    // (0, 0) is smallest (x, z)
    // If out of bounds, throws error
    private int[] positionToTileCoord(Vector3 position)
    {
        Vector3 loc = ((position - bottomLeftCorner) / tileSize);
        if(loc.x >= height || loc.z >= width) {
            throw new System.Exception("World position does not correspond to a tile");
        }
        return new int[] { (int) loc.x, (int) loc.z };
    }

    void destroyTile(Vector3 location)
    {
        int[] coords = positionToTileCoord(location);
        tiles[coords[0]][coords[1]].SetActive(false);
    }

    void destroyTiles(Vector3 location, float radius)
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FloorController : NetworkBehaviour
{
    public int width, height;
    public float tileSize;
    public GameObject tilePrefab;
    public GameObject spawnPointPrefab;
    private GameObject[][] tiles;
    // The floor object is the center of the floor, but the tiles matrix is relative to the bottom left corner.
    private Vector3 bottomLeftCorner;

    public override void OnStartServer()
    {
        tileSize = 4;
        tiles = new GameObject[height][];
        bottomLeftCorner = transform.position - new Vector3(height/2f*tileSize, 0, width/2f*tileSize);
        generateFloor();
    }

    [Server]
    void generateFloor()
    {
        for (int r = 0; r < height; r++)
        {
            tiles[r] = new GameObject[width];
            for (int c = 0; c < width; c++)
            {
                Vector3 offset = new Vector3(r * tileSize, 0, c * tileSize);
                GameObject tile = Instantiate(tilePrefab, bottomLeftCorner + offset, Quaternion.identity);
                //Scale is broken woth network spawn
                //tile.transform.localScale *= tileSize;
                NetworkServer.Spawn(tile);
                tiles[r][c] = tile;
                //Spawnpoints around edges
                if(r == 0 || c == 0 || r == height-1 || c == width-1)
                {
                    //Only instatiated locally (on server)
                    GameObject spawnPoint = Instantiate(spawnPointPrefab, bottomLeftCorner + offset, Quaternion.identity);
                }
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

    [Server]
    void destroyTile(Vector3 location)
    {
        int[] coords = positionToTileCoord(location);
        tiles[coords[0]][coords[1]].SetActive(false);
    }
}

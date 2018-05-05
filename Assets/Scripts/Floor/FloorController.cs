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
                    Instantiate(spawnPointPrefab, bottomLeftCorner + offset, Quaternion.identity);
                }
            }
        }
    }


    // Position is in world coordinates. Coord is in tile matrix.
    // (0, 0) is smallest (x, z)
    // If out of bounds, throws error
    public int[] PositionToTileCoord(Vector3 position)
    {
        Vector3 loc = ((position - bottomLeftCorner) / tileSize);
        if(loc.x >= height || loc.z >= width) {
            throw new System.Exception("World position does not correspond to a tile");
        }
        return new int[] { (int) loc.x, (int) loc.z };
    }

    public Vector3 TileCoordToPosition(int x, int y)
    {
        if(x < 0 || y < 0 || x >= width || y >= height)
        {
            throw new System.Exception("Tile coordinates out of bounds");
        }
        return bottomLeftCorner + new Vector3(x*tileSize, 0, y*tileSize);
    }

    /// <returns>A random position on the floor, not too close to edges</returns>
    public Vector3 GetRandomPosition()
    {
        float edgeDistance = .5f;
        return bottomLeftCorner + new Vector3(Random.Range(edgeDistance, width*tileSize - edgeDistance), 0, 
            Random.Range(edgeDistance, height * tileSize - edgeDistance));
    }

    [Server]
    void destroyTile(Vector3 location)
    {
        int[] coords = PositionToTileCoord(location);
        tiles[coords[0]][coords[1]].SetActive(false);
    }
}

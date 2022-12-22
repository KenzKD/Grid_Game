using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public ObstacleData obstacleData; // Reference to the scriptable object that stores the obstacle data
    public TileInformation[,] tiles; // 2D array of TileInformation objects representing the grid

    private Vector2Int currentTile,targetTile; // The current tile position of the player
    private bool isMoving; // Flag to indicate if the player is currently moving
    public Camera cam;

    void Start()
    {
        // Initialize the currentTile variable to the player's starting position
        currentTile = new Vector2Int((int)transform.position.x, (int)transform.position.y);

        // Initialize the tiles array with the TileInformation objects from the scene
        tiles = new TileInformation[10, 10];
        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                tiles[x, y] = GameObject.Find($"Tile {x} {y}").GetComponent<TileInformation>();
            }
        }
    }

    void Update()
    {
        
        Vector3 Offset = new Vector3(0, 0, 5);

        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                // If the raycast hits a cube, display its information
                TileInformation info = hit.collider.gameObject.GetComponent<TileInformation>();
                if (info != null && !obstacleData.grid[info.tilePosition.x, info.tilePosition.y])
                {
                    targetTile = info.tilePosition;
                    StartCoroutine(MoveToTile());
                    Debug.Log("click on " + targetTile);
                }
            }
        }
    }

    IEnumerator MoveToTile()
    {
        // Reset the previousTile variable of all tiles
        for (int x = 0; x < tiles.GetLength(0); x++)
        {
            for (int y = 0; y < tiles.GetLength(1); y++)
            {
                tiles[x, y].previousTile = null;
            }
        }

        // Add the current tile to the visited tiles list and tile queue
        List<TileInformation> visitedTiles = new List<TileInformation>();
        Queue<TileInformation> tileQueue = new Queue<TileInformation>();
        visitedTiles.Add(tiles[currentTile.x, currentTile.y]);
        tileQueue.Enqueue(tiles[currentTile.x, currentTile.y]);

        while (tileQueue.Count > 0)
        {
            // Dequeue the next tile from the queue
            TileInformation tile = tileQueue.Dequeue();

            // Check if the tile is the target tile
            if (tile.tilePosition == targetTile)
            {
                // The target tile has been found, so start the FollowPath coroutine
                yield return StartCoroutine(FollowPath());
                yield break;
            }

            // Get a list of the tile's neighbors
            List<TileInformation> neighbors = GetNeighbors(tile);

            // Loop through each neighbor
            foreach (TileInformation neighbor in neighbors)
            {
                // Check if the neighbor is an obstacle or if it has already been visited
                if (obstacleData.grid[neighbor.tilePosition.x, neighbor.tilePosition.y] || visitedTiles.Contains(neighbor))
                {
                    continue;
                }

                // Mark the neighbor as visited and store the current tile as its previous tile
                visitedTiles.Add(neighbor);
                neighbor.previousTile = tile;

                // Add the neighbor to the queue
                tileQueue.Enqueue(neighbor);
            }

            // Wait for the next frame before continuing the loop
            yield return null;
        }
    }

    // This function returns a list of the tiles that are neighbors of the given tile
    List<TileInformation> GetNeighbors(TileInformation tile)
    {
        List<TileInformation> neighbors = new List<TileInformation>();

        // Check the left, right, top, and bottom neighbors
        if (tile.tilePosition.x > 0)
        {
            neighbors.Add(tiles[tile.tilePosition.x - 1, tile.tilePosition.y]);
        }
        if (tile.tilePosition.x < tiles.GetLength(0) - 1)
        {
            neighbors.Add(tiles[tile.tilePosition.x + 1, tile.tilePosition.y]);
        }
        if (tile.tilePosition.y > 0)
        {
            neighbors.Add(tiles[tile.tilePosition.x, tile.tilePosition.y - 1]);
        }
        if (tile.tilePosition.y < tiles.GetLength(1) - 1)
        {
            neighbors.Add(tiles[tile.tilePosition.x, tile.tilePosition.y + 1]);
        }

        return neighbors;
    }

    IEnumerator FollowPath()
    {
        // Create a list to store the path
        List<Vector2Int> path = new List<Vector2Int>();

        // Trace the path from the target tile back to the current tile
        TileInformation tile = tiles[targetTile.x, targetTile.y];
        while (tile.tilePosition != currentTile)
        {
            path.Add(tile.tilePosition);
            tile = tile.previousTile;
        }

        // Reverse the path to get the correct order
        path.Reverse();

        // Loop through the path and move the player to each tile
        foreach (Vector2Int t in path)
        {
            // Move the player to the next tile
            transform.position = new Vector3(t.x, (float)1.5,t.y);

            // Wait for the next frame before continuing the loop
            yield return null;
        }
    }
}
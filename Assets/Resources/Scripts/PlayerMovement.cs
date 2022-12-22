using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour,AI
{
    public ObstacleData obstacleData;
    public TileInformation[,] tiles;
    public Vector2Int currentTile, targetTile;
    public bool isMoving = false;
    public bool isReached = false;
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
        // Check if the left mouse button has been clicked and the player is not currently moving
        if (Input.GetMouseButtonDown(0) && !isMoving)
        {
            // Create a ray from the main camera to the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Check if the raycast hits a collider
            if (Physics.Raycast(ray, out hit))
            {
                // Get the TileInformation component of the hit object
                TileInformation info = hit.collider.gameObject.GetComponent<TileInformation>();

                // Check if the hit object has a TileInformation component and is not an obstacle
                if (info != null && !obstacleData.grid[info.tilePosition.x, info.tilePosition.y])
                {
                    // Set the target tile to the position of the hit object
                    targetTile = info.tilePosition;
                    Debug.Log("click on " + targetTile);

                    isReached = false;
                    // Start the MoveToTile coroutine
                    StartCoroutine(MoveToTile());

                }
            }
        }
    }

    public IEnumerator MoveToTile()
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

    // This function makes the Game Object move along the Chosen Path
    public IEnumerator FollowPath()
    {
        isMoving = true;
        // Create a list to store the path
        List<Vector2Int> path = CalculatePath();

        // Loop through the path and move the player to each tile
        foreach (Vector2Int t in path)
        {
            Vector3 targetPosition = new Vector3(t.x, 1.5f, t.y);
            while (transform.position != targetPosition)
            {
                //Move the player to the next Tile
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * 5f);
                // Wait for the next frame before continuing the loop
                yield return null;
            }
            
            // Update the currentTile variable
            currentTile = targetTile;
        }
        isMoving = false;
        isReached = true;
    }

    // This function returns a list of the tiles that are neighbors of the given tile
    public List<TileInformation> GetNeighbors(TileInformation tile)
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

    // This function returns a list of the optimal path the object should follow to reach the target.
    public List<Vector2Int> CalculatePath()
    {
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
        return path;
    }
}
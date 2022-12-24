using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public ObstacleData obstacleData;
    private GameObject[,] grid;

    void Start()
    {
        // Initialize the grid array
        grid = new GameObject[10, 10];
    }

    void Update()
    {
        // Loop through each tile in the grid
        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                // Check if the current tile is an obstacle and there is no obstacle GameObject at that position
                if (obstacleData.grid[x, y] && grid[x, y] == null)
                {
                    // Instantiate an obstacle GameObject at the current tile position
                    grid[x, y] = Instantiate(obstacleData.obstaclePrefab, new Vector3(x, 1, y), Quaternion.identity);
                }
                // Check if the current tile is not an obstacle and there is an obstacle GameObject at that position
                else if (!obstacleData.grid[x, y] && grid[x, y] != null)
                {
                    // Destroy the obstacle GameObject at the current tile position
                    Destroy(grid[x, y]);

                    // Set the grid element to null
                    grid[x, y] = null;
                }
            }
        }
    }
}
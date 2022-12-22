using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public ObstacleData obstacleData;

    private GameObject[,] grid;

    void Start()
    {
        grid = new GameObject[10, 10];

        // for (int x = 0; x < 10; x++)
        // {
        //     for (int y = 0; y < 10; y++)
        //     {
        //         if (obstacleData.grid[x, y])
        //         {
        //             grid[x, y] = Instantiate(obstacleData.obstaclePrefab, new Vector3(x, 1, y), Quaternion.identity);
        //         }
        //     }
        // }
    }

    void Update()
    {
        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                if (obstacleData.grid[x, y] && grid[x, y] == null)
                {
                    grid[x, y] = Instantiate(obstacleData.obstaclePrefab, new Vector3(x, 1, y), Quaternion.identity);
                }
                else if (!obstacleData.grid[x, y] && grid[x, y] != null)
                {
                    Destroy(grid[x, y]);
                    grid[x, y] = null;
                }
            }
        }
    }
}


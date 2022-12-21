using UnityEditor;
using UnityEngine;

public class GridWindow : EditorWindow
{
    public GameObject cubePrefab,obstacle;
    
    public float tileSize = 1.0f;

    [MenuItem("Window/Grid Window")]

    public static void ShowWindow()
    {
        GetWindow<GridWindow>().Show();
    }

    void OnGUI()
    {
        cubePrefab = (GameObject)EditorGUILayout.ObjectField("Cube Prefab", cubePrefab, typeof(GameObject), false);
        obstacle = (GameObject)EditorGUILayout.ObjectField("Obstacle Prefab", obstacle, typeof(GameObject), false);
        tileSize = EditorGUILayout.FloatField("Tile Size", tileSize);

        if (GUILayout.Button("Generate Grid"))
        {
            GenerateGrid();
        }
    }

    void GenerateGrid()
    {
        var Grid = new GameObject("Grid");
        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                Vector3 position = new Vector3(x * tileSize, 0, y * tileSize);
                Vector3 offset = new Vector3(x * tileSize, 1, y * tileSize);
                GameObject cube = Instantiate(cubePrefab, position, Quaternion.identity, Grid.transform) as GameObject;

                TileInformation info = cube.AddComponent<TileInformation>();
                info.name = $"Tile {x} {y}";
                info.tilePosition = new Vector2Int(x, y);
            }
        }
    }
}


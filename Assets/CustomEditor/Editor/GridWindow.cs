using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GridWindow : EditorWindow
{
    // The prefab for the cubes
    public GameObject cubePrefab;

    // The size of each tile in the grid
    public float tileSize = 1.0f;
    //public Camera camera;

    // The text mesh that will display the tile information
    //public TextMesh infoText;

    [MenuItem("Window/Grid Window")]
    public static void ShowWindow()
    {
        // Show the window
        GetWindow<GridWindow>().Show();
    }

    void OnGUI()
    {
        // Display the fields for the prefab, tile size, and info text
        cubePrefab = (GameObject)EditorGUILayout.ObjectField("Cube Prefab", cubePrefab, typeof(GameObject), false);
        tileSize = EditorGUILayout.FloatField("Tile Size", tileSize);
        //infoText = (TextMesh)EditorGUILayout.ObjectField("Info Text", infoText, typeof(TextMesh), true);
        //camera = (Camera)EditorGUILayout.ObjectField("Camera", camera, typeof(Camera), true);
        // Display a button to generate the grid
        if (GUILayout.Button("Generate Grid"))
        {
            GenerateGrid();
        }

        // Do a raycast from the camera position
        //Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        // Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
        // RaycastHit hit;
        // Vector3 Offset = new Vector3(0, 0, 5);

        // if (Physics.Raycast(ray, out hit))
        // {
        //     // If the raycast hits a cube, display its information
        //     TileInformation info = hit.collider.gameObject.GetComponent<TileInformation>();
        //     if (info != null)
        //     {
        //         // Set the position of the info text to the position of the tile
        //         infoText.transform.position = hit.collider.gameObject.transform.position+Offset;

        //         // Set the text of the info text to the tile position
        //         infoText.text = "Tile Position: " + info.tilePosition;
        //     }
        //     else
        //     {
        //         infoText.text = "";
        //     }
        // }
    }

    void GenerateGrid()
    {
        var Grid = new GameObject("Grid");
        // Loop through the grid and instantiate a cube at each position
        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                // Calculate the position of the cube
                Vector3 position = new Vector3(x * tileSize, 0, y * tileSize);

                // Instantiate the cube
                GameObject cube = Instantiate(cubePrefab, position, Quaternion.identity, Grid.transform) as GameObject;

                // Add a TileInformation script to the cube
                TileInformation info = cube.AddComponent<TileInformation>();
                info.tilePosition = new Vector2Int(x, y);
            }
        }
    }
}


using UnityEditor;

public class ObstacleEditorWindow : EditorWindow
{
    public ObstacleData data;

    [MenuItem("Window/Obstacle Editor")]
    public static void ShowWindow()
    {
        GetWindow<ObstacleEditorWindow>().Show();
    }

    void OnGUI()
    {
        data = (ObstacleData)EditorGUILayout.ObjectField("Obstacle Data", data, typeof(ObstacleData), false);

        // If the data object is not null,
        // create a toggle grid in the inspector for the user 
        //to specify which tiles are obstacles
        if (data != null)
        {
            for (int x = 0; x < 10; x++)
            {
                EditorGUILayout.BeginHorizontal();
                for (int y = 0; y < 10; y++)
                {
                    // Display a toggle for each tile
                    data.grid[x, y] = EditorGUILayout.Toggle(data.grid[x, y]);
                }
                EditorGUILayout.EndHorizontal();
            }
        }
    }
}
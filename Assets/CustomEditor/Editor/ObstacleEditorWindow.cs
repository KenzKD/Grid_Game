using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ObstacleEditorWindow : EditorWindow
{
    public ObstacleData data;
    // Toggle[,] gridToggles;

    [MenuItem("Window/Obstacle Editor")]
    public static void ShowWindow()
    {
        GetWindow<ObstacleEditorWindow>().Show();
    }

    void OnGUI()
    {
        data = (ObstacleData)EditorGUILayout.ObjectField("Obstacle Data", data, typeof(ObstacleData), false);

        if (data != null)
        {
            for (int x = 0; x < 10; x++)
            {
                EditorGUILayout.BeginHorizontal();
                for (int y = 0; y < 10; y++)
                {
                    data.grid[x, y] = EditorGUILayout.Toggle(data.grid[x, y]);
                }
                EditorGUILayout.EndHorizontal();
            }
        }
    }
    // [SerializeField] private ObstacleData data;
    // SerializedProperty gridProperty;

    // [MenuItem("Window/Obstacle Editor")]
    // public static void ShowWindow()
    // {
    //     GetWindow<ObstacleEditorWindow>().Show();
    // }

    // void OnEnable()
    // {
    //     gridProperty = serializedObject.FindProperty("grid");
    // }

    // void OnGUI()
    // {
    //     //data = (ObstacleData)EditorGUILayout.ObjectField("Obstacle Data", data, typeof(ObstacleData), false);
    //     EditorGUILayout.PropertyField(serializedObject.FindProperty("data"), new GUIContent("Obstacle Data"), true);

    //     if (data != null)
    //     {
    //         for (int x = 0; x < 10; x++)
    //         {
    //             EditorGUILayout.BeginHorizontal();
    //             for (int y = 0; y < 10; y++)
    //             {
    //                 gridProperty.GetArrayElementAtIndex(x * 10 + y).boolValue = EditorGUILayout.Toggle(gridProperty.GetArrayElementAtIndex(x * 10 + y).boolValue);
    //             }
    //             EditorGUILayout.EndHorizontal();
    //         }
    //     }

    //     serializedObject.ApplyModifiedProperties();
    // }

    
    // void OnGUI()
    // {
    //     data = (ObstacleData)EditorGUILayout.ObjectField("Obstacle Data", data, typeof(ObstacleData), false);

    //     if (data != null)
    //     {
    //         if (gridToggles == null)
    //         {
    //             gridToggles = new Toggle[10, 10];
    //             for (int x = 0; x < 10; x++)
    //             {
    //                 for (int y = 0; y < 10; y++)
    //                 {
    //                     gridToggles[x, y] = CreateToggle(x, y);
    //                 }
    //             }
    //         }

    //         for (int x = 0; x < 10; x++)
    //         {
    //             EditorGUILayout.BeginHorizontal();
    //             for (int y = 0; y < 10; y++)
    //             {
    //                 gridToggles[x, y].isOn = EditorGUILayout.Toggle(gridToggles[x, y].isOn);
    //             }
    //             EditorGUILayout.EndHorizontal();
    //         }
    //     }
    // }

    // Toggle CreateToggle(int x, int y)
    // {
    //     Rect rect = new Rect(x * 20, y * 20, 20, 20);

    //     Toggle toggle = new Toggle();
    //     toggle.transform.position = rect.position;
    //     toggle.transform.sizeDelta = rect.size;

    //     toggle.onValueChanged.AddListener((value) => UpdateObstacleData(x, y, value));

    //     return toggle;
    // }

    // void UpdateObstacleData(int x, int y, bool value)
    // {
    //     data.grid[x, y] = value;
    //     EditorUtility.SetDirty(data);
    // }
}
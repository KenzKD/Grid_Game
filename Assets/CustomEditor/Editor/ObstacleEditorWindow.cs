
using System;
using UnityEditor;
using UnityEngine;

public class ObstacleEditorWindow : EditorWindow
{
    private ObstacleData data;
    private bool[,] toggleValues;
    private int gridWidth;
    private int gridHeight;

    [MenuItem("Window/Obstacle Editor")]
    public static void ShowWindow()
    {
        GetWindow<ObstacleEditorWindow>().Show();
    }

    // private void OnEnable()
    // {
    //     // Register for the playModeStateChanged event
    //     EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    //     Debug.Log("OnPlayModeStateChanged event registered");

    // }

    // private void OnDisable()
    // {
    //     // Unregister from the playModeStateChanged event
    //     EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
    //     Debug.Log("OnPlayModeStateChanged event unregistered");

    // }

    // private void OnPlayModeStateChanged(PlayModeStateChange state)
    // {
    //     Debug.Log("OnPlayModeStateChanged called with state: " + state);

    //     if (state == PlayModeStateChange.ExitingPlayMode || state == PlayModeStateChange.EnteredEditMode || state == PlayModeStateChange.ExitingEditMode || state == PlayModeStateChange.EnteredPlayMode)
    //     {
    //         // Update the toggle values with the values from data.grid before saving the scriptable object
    //         Array.Copy(data.grid, toggleValues, data.grid.Length);

    //         // Save the toggle values to the scriptable object when exiting play mode, entering edit mode, or exiting edit mode
    //         if (data != null)
    //         {
    //             Undo.RecordObject(data, "Grid Value Changed");
    //             Array.Copy(toggleValues, data.grid, toggleValues.Length);
    //             EditorUtility.SetDirty(data);
    //         }
    //     }
    //     // else if (state == PlayModeStateChange.EnteredPlayMode)
    //     // {
    //     //     // Load the toggle values from the scriptable object when entering play mode
    //     //     if (data != null)
    //     //     {
    //     //         // Update the data.grid field with the values from toggleValues before entering play mode
    //     //         Array.Copy(toggleValues, data.grid, toggleValues.Length);
    //     //         EditorUtility.SetDirty(data);
    //     //     }
    //     // }
    // }

    void OnGUI()
    {
        data = (ObstacleData)EditorGUILayout.ObjectField("Scriptable Object", data, typeof(ObstacleData), false);
        if (data != null)
        {
            // Initialize the toggle values with the values from data.grid whenever the scriptable object is selected
            if (toggleValues == null || toggleValues.GetLength(0) != data.grid.GetLength(0) || toggleValues.GetLength(1) != data.grid.GetLength(1))
            {
                toggleValues = new bool[data.grid.GetLength(0), data.grid.GetLength(1)];
                Array.Copy(data.grid, toggleValues, data.grid.Length);
            }

            gridWidth = data.grid.GetLength(0);
            gridHeight = data.grid.GetLength(1);

            for (int x = 0; x < gridWidth; x++)
            {
                EditorGUILayout.BeginHorizontal();
                for (int y = 0; y < gridHeight; y++)
                {
                    data.grid[x, y] = EditorGUILayout.Toggle(data.grid[x, y]);
                    toggleValues[x, y] = data.grid[x, y];
                }
                EditorGUILayout.EndHorizontal();
            }

            if (GUI.changed)
            {
                Undo.RecordObject(data, "Grid Value Changed");
                data.grid = toggleValues;
                EditorUtility.SetDirty(data);
                AssetDatabase.SaveAssetIfDirty(data);
            }
        }
    }



    // void OnGUI()
    // {
    //     data = (ObstacleData)EditorGUILayout.ObjectField("Obstacle Data", data, typeof(ObstacleData), false);

    //     // If the data object is not null,
    //     // create a toggle grid in the inspector for the user 
    //     //to specify which tiles are obstacles
    //     if (data != null)
    //     {
    //         for (int x = 0; x < 10; x++)
    //         {
    //             EditorGUILayout.BeginHorizontal();
    //             for (int y = 0; y < 10; y++)
    //             {
    //                 // Display a toggle for each tile
    //                 data.grid[x, y] = EditorGUILayout.Toggle(data.grid[x, y]);
    //             }
    //             EditorGUILayout.EndHorizontal();
    //         }
    //     }
    // }
}
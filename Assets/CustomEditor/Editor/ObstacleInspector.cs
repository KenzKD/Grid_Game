
// using UnityEngine;
// using System;
// using UnityEditor;

// [CustomEditor(typeof(ObstacleData))]
// public class ObstacleInspectorEditor : Editor
// {
//     [SerializeField] private ObstacleData data;
//     private bool[,] toggleValues;
//     private int gridWidth;
//     private int gridHeight;

//     public override void OnInspectorGUI()
//     {
//         data = (ObstacleData)EditorGUILayout.ObjectField("Scriptable Object", data, typeof(ObstacleData), false);
//         if (data != null)
//         {
//             // Initialize the toggle values with the values from data.grid whenever the scriptable object is selected
//             if (toggleValues == null || toggleValues.GetLength(0) != data.grid.GetLength(0) || toggleValues.GetLength(1) != data.grid.GetLength(1))
//             {
//                 toggleValues = new bool[data.grid.GetLength(0), data.grid.GetLength(1)];
//                 Array.Copy(data.grid, toggleValues, data.grid.Length);
//             }

//             gridWidth = data.grid.GetLength(0);
//             gridHeight = data.grid.GetLength(1);

//             for (int x = 0; x < gridWidth; x++)
//             {
//                 EditorGUILayout.BeginHorizontal();
//                 for (int y = 0; y < gridHeight; y++)
//                 {
//                     data.grid[x, y] = EditorGUILayout.Toggle(data.grid[x, y]);
//                     toggleValues[x, y] = data.grid[x, y];
//                 }
//                 EditorGUILayout.EndHorizontal();
//             }

//             if (GUI.changed)
//             {
//                 Undo.RecordObject(data, "Grid Value Changed");
//                 data.grid = toggleValues;
//                 EditorUtility.SetDirty(data);
//                 AssetDatabase.SaveAssets();
//             }
//         }
//     }
//     // if (serializedObject == null)
//     // {
//     //     Debug.LogError("serializedObject is null");
//     //     return;
//     // }

//     // serializedObject.Update();

//     // if (serializedObject.targetObject == null)
//     // {
//     //     Debug.LogError("serializedObject.targetObject is null");
//     //     return;
//     // }

//     // data = serializedObject.targetObject as ObstacleData;

//     // if (data == null)
//     // {
//     //     Debug.LogError("data is null");
//     //     return;
//     // }

//     // if (data.grid == null)
//     // {
//     //     Debug.LogError("data.grid is null");
//     //     return;
//     // }


//     //     // Initialize the toggle values with the values from data.grid whenever the scriptable object is selected
//     //     toggleValues = new bool[serializedObject.FindProperty("grid").arraySize, serializedObject.FindProperty("grid.Array.size1").intValue];
//     //     for (int x = 0; x < toggleValues.GetLength(0); x++)
//     //     {
//     //         for (int y = 0; y < toggleValues.GetLength(1); y++)
//     //         {
//     //             var property = serializedObject.FindProperty(string.Format("grid.Array.data[{0},{1}]", x, y));
//     //             if (property == null)
//     //             {
//     //                 continue;
//     //             }
//     //             toggleValues[x, y] = property.boolValue;
//     //         }
//     //     }

//     //     gridWidth = toggleValues.GetLength(0);
//     //     gridHeight = toggleValues.GetLength(1);

//     //     for (int x = 0; x < gridWidth; x++)
//     //     {
//     //         EditorGUILayout.BeginHorizontal();
//     //         for (int y = 0; y < gridHeight; y++)
//     //         {
//     //             toggleValues[x, y] = EditorGUILayout.Toggle(toggleValues[x, y]);
//     //         }
//     //         EditorGUILayout.EndHorizontal();
//     //     }

//     //     if (GUI.changed)
//     //     {
//     //         // Update the serialized property with the new values from toggleValues
//     //         for (int x = 0; x < toggleValues.GetLength(0); x++)
//     //         {
//     //             for (int y = 0; y < toggleValues.GetLength(1); y++)
//     //             {
//     //                 serializedObject.FindProperty(string.Format("grid.Array.data[{0},{1}]", x, y)).boolValue = toggleValues[x, y];
//     //             }
//     //         }
//     //         serializedObject.ApplyModifiedProperties();
//     //     }
//     // }





//     // private void OnEnable()
//     // {
//     //     // Register for the playModeStateChanged event
//     //     EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
//     //     Debug.Log("OnPlayModeStateChanged event registered");

//     // }

//     // private void OnDisable()
//     // {
//     //     // Unregister from the playModeStateChanged event
//     //     EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
//     //     Debug.Log("OnPlayModeStateChanged event unregistered");
//     // }

//     // private void OnPlayModeStateChanged(PlayModeStateChange state)
//     // {
//     //     Debug.Log("OnPlayModeStateChanged called with state: " + state);

//     //     switch (state)
//     //     {
//     //         case PlayModeStateChange.ExitingPlayMode:
//     //         case PlayModeStateChange.EnteredEditMode:
//     //         case PlayModeStateChange.ExitingEditMode:
//     //             // Update the toggle values with the values from data.grid before saving the scriptable object
//     //             Array.Copy(data.grid, toggleValues, data.grid.Length);

//     //             // Save the toggle values to the scriptable object when exiting play mode, entering edit mode, or exiting edit mode
//     //             if (data != null)
//     //             {
//     //                 Undo.RecordObject(data, "Grid Value Changed");
//     //                 Array.Copy(toggleValues, data.grid, toggleValues.Length);
//     //                 EditorUtility.SetDirty(data);
//     //             }
//     //             break;
//     //         case PlayModeStateChange.EnteredPlayMode:
//     //             // Load the toggle values from the scriptable object when entering play mode
//     //             if (data != null)
//     //             {
//     //                 // Update the data.grid field with the values from toggleValues before entering play mode
//     //                 Array.Copy(toggleValues, data.grid, toggleValues.Length);
//     //                 EditorUtility.SetDirty(data);
//     //             }
//     //             break;
//     //     }
//     // }

// }
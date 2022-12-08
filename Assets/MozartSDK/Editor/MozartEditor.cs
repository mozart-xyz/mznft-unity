using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Simple script that lets you create a new
// Scene, create a cube and an empty game object in the Scene
// Save the Scene and close the editor

using UnityEditor;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
class MyHierarchyIcon
{
    static Texture2D texture;
    static List<int> markedObjects;

    static MyHierarchyIcon()
    {
        // Init
        texture = AssetDatabase.LoadAssetAtPath("Assets/MozartSDK/mzicon.png", typeof(Texture2D)) as Texture2D;
        EditorApplication.hierarchyWindowItemOnGUI += HandleHierarchyWindowItemOnGUI; //HierarchyItemCB;
    }

    static void HandleHierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
    {
        GameObject g = EditorUtility.InstanceIDToObject(instanceID) as GameObject;

        if (g != null && (g.GetComponent<Mozart.MozartBehaviorBase>() != null || g.GetComponent<Mozart.MozartManager>() != null))
            GUI.DrawTexture(new Rect(selectionRect.xMax - 16, selectionRect.yMin, 16, 16), texture);

    }
}

public class MozartSettings : EditorWindow
{
    void OnGUI()
    {
        var settings = Resources.FindObjectsOfTypeAll<Mozart.SettingsTemplate>()[0];
        settings.apiBaseUrl = EditorGUILayout.TextField("API URL", settings.apiBaseUrl);
        settings.GameIdentifier = EditorGUILayout.TextField("Game ID", settings.GameIdentifier);
        if (GUILayout.Button("Documentation"))
        {
            Application.OpenURL("https://docs.mozart.xyz/docs/quickstart");
        }
    }
}

public class MozartEditor
{
    [MenuItem("Mozart/Settings")]
    static void EditorPlaying()
    {
        MozartSettings window = (MozartSettings)EditorWindow.GetWindow(typeof(MozartSettings));
        window.Show();
    }


}

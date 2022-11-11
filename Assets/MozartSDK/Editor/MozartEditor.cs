using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Simple script that lets you create a new
// Scene, create a cube and an empty game object in the Scene
// Save the Scene and close the editor

using UnityEditor;
using UnityEditor.SceneManagement;

public class MozartEditor
{
    [MenuItem("Mozart/FindSettings")]
    static void EditorPlaying()
    {
        var newScene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);

        EditorApplication.ExecuteMenuItem("GameObject/3D Object/Cube");
        EditorApplication.ExecuteMenuItem("GameObject/Create Empty");
        
        EditorSceneManager.SaveScene(newScene, "Assets/MyNewScene.unity");
        EditorApplication.Exit(0);
    }
}

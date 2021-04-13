using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnvironmentAssetSwapper : EditorWindow
{
    enum WorldType
    {
        normal,
        type1,
        type2,
        type3
    }

    readonly int spacing = 7;

    Vector2 scrollPos;

    WorldType type = WorldType.normal;

    [MenuItem("Window/Environment Asset Swapper")]
    public static void ShowWindow()
    {
        EditorWindow window = GetWindow(typeof(EnvironmentAssetSwapper), false, "Asset Swapper");
        window.minSize = new Vector2(200f, 200f);
        window.Show();
    }

    private void OnGUI()
    {
        EditorGUILayout.BeginVertical();
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, false, false);

        GUILayout.Label("Room Prefab", EditorStyles.boldLabel);

        EditorGUILayout.Space(spacing);

        

    }

}

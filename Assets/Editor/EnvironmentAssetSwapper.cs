using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnvironmentAssetSwapper : EditorWindow
{
    public enum WorldType
    {
        normal,
        type1,
        type2,
        type3
    }

    readonly int spacing = 7;

    Vector2 scrollPos;

    public WorldType type = WorldType.normal;

    GameObject roomPrefab;

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
        roomPrefab = (GameObject)EditorGUILayout.ObjectField("Room to edit", roomPrefab, typeof(GameObject), false);

        EditorGUILayout.Space(spacing);

        type = (WorldType)EditorGUILayout.EnumPopup("World Type:", type);

        if (roomPrefab != null && GUILayout.Button("Update Assets"))
        {
            UpdateAssets();
        }

        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();

    }


    void UpdateAssets()
    {
        GameObject roomScript = roomPrefab.transform.Find("Room Script").gameObject;

        foreach (Transform obj in roomScript.transform)
        {


            if (obj.name == "Doors")
            {

            }
            else if (obj.name == "Walls")
            {
                if (obj.tag == "Tree" || obj.tag == "Rock")
                {

                }
            }
        }

    }

}

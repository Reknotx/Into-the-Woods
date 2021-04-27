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

    AssetsForSwapping assetsSwap;

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

        EditorGUILayout.Space(spacing);

        assetsSwap = (AssetsForSwapping)EditorGUILayout.ObjectField("Assets to swap with", assetsSwap, typeof(AssetsForSwapping), false);

        if (roomPrefab != null && assetsSwap != null && GUILayout.Button("Update Assets"))
        {
            UpdateAssets();
        }

        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();

    }


    void UpdateAssets()
    {

        string assetPath = AssetDatabase.GetAssetPath(roomPrefab);

        ///The room prefab instance.
        GameObject loadedPrefab = PrefabUtility.LoadPrefabContents(assetPath);

        GameObject roomScript = loadedPrefab.transform.Find("Room Script").gameObject;

        foreach (Transform obj in roomScript.transform)
        {


            if (obj.name == "Doors")
            {
                
            }
            else if (obj.name == "Walls")
            {
                List<GameObject> remove = new List<GameObject>();

                foreach (Transform tree in obj.transform)
                {
                    if (tree.tag == "tree")
                    {
                        string newAssetPath = AssetDatabase.GetAssetPath(assetsSwap.trees[Random.Range(0, assetsSwap.trees.Count)]);

                        //Debug.Log(newAssetPath);

                        //Debug.Log(PrefabUtility.LoadPrefabContents(newAssetPath).name);
                        GameObject tempP = PrefabUtility.LoadPrefabContents(newAssetPath);

                        GameObject tempAsset = Instantiate(tempP, obj);

                        tempAsset.transform.position = tree.position;

                        //PrefabUtility.UnpackPrefabInstance(tree.gameObject, PrefabUnpackMode.Completely, InteractionMode.UserAction);
                        
                        remove.Add(tree.gameObject);
                        PrefabUtility.UnloadPrefabContents(tempP);

                    }
                }

                foreach (GameObject delete in remove)
                {
                    DestroyImmediate(delete);
                }

                remove.Clear();

            }
        }

        PrefabUtility.SaveAsPrefabAsset(loadedPrefab, assetPath);
        PrefabUtility.UnloadPrefabContents(loadedPrefab);

    }

}

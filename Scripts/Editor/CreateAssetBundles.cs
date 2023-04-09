using System;
using UnityEngine;
using UnityEditor;


public class CreateAssetBundles
{
    [MenuItem("Assets/Create Assets Bundles")]
    private static void BuildAllAssetBundles()
    {
        string path = Application.dataPath + "/../AssetBundles";
        try
        {
            BuildPipeline.BuildAssetBundles(path, BuildAssetBundleOptions.None, EditorUserBuildSettings.activeBuildTarget);
        }
        catch (Exception e)
        {
            UnityEngine.Debug.LogWarning(e);
        }
    }
}

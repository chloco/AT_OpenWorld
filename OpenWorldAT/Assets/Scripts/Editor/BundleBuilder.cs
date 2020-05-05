using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BundleBuilder : Editor
{
    [MenuItem("Assets/ Build AssetBundles Baby")]
    static void BuildAllAssetBundles()
    {
        BuildPipeline.BuildAssetBundles(@"C:\Users\Chloee\Documents\AT_OpenWorld\OpenWorldAT\AssetBundles", BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.StandaloneWindows64);
    }

}

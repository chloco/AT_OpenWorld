using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LoadAssetBundles : MonoBehaviour
{
    AssetBundle myBundle;
    public string path;
    string jsonString;

    [System.Serializable]
    public class ObjectData
    {
        public int id;
        public string assetName;
        public Vector3 objectPosition;
        public Vector3 objectRotation;
        public Vector3 objectScale;
        
    }

    void Start()
    {
        path = Application.streamingAssetsPath + "/Objects.json";
        jsonString = File.ReadAllText(path);
        ObjectData objectData = JsonUtility.FromJson<ObjectData>(jsonString);
        InstantiateObjectFromBundle(objectData.assetName);
    }

    private ObjectData createObjectSave()
    {
        ObjectData objectData = new ObjectData();

        return objectData;

    }

        void LoadAssetBundle(string bundleURL)
    {
        myBundle = AssetBundle.LoadFromFile(bundleURL);
    }

    void InstantiateObjectFromBundle(string assetName)
    {
        var prefab = myBundle.LoadAsset(assetName);
    }
}

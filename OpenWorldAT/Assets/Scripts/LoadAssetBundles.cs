using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;

public class LoadAssetBundles : MonoBehaviour
{
    AssetBundle myBundle;
    public string path;
    string jsonString;


    public float id;
    public string assetName;
    public Vector3 objectPosition;
    public Vector3 objectRotation;
    public Vector3 objectScale;

    [SerializeField]
    private List<ObjectData> objectData;

    [System.Serializable]
    public class ObjectData
    {
        public float id;
        public string assetName;
    public Vector3 objectPosition;
    public Vector3 objectRotation;
    public Vector3 objectScale;
        
    }

    void Start()
    {
        LoadAssetBundle(path);
    }


    //private ObjectData createObjectSave()
    //{
    //    ObjectData objectData = new ObjectData();

    //    return objectData;

    //}

    void LoadAssetBundle(string bundleURL)
    {
        myBundle = AssetBundle.LoadFromFile(bundleURL);

        Debug.Log(myBundle == null ? "Failed" : "Succession");
    }

    public GameObject InstantiateObjectFromBundle(string assetName)
    {
        var prefab = myBundle.LoadAsset(assetName);

        return (GameObject)prefab;
    }

    public GameObject InstantiateObjectFromBundle2(string assetName)
    {
        var prefab = myBundle.LoadAsset(assetName);

        GameObject g = Instantiate(myBundle.LoadAsset(assetName)) as GameObject;
        return g;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 [System.Serializable]
public class ClassCollector
{
    public List<Save> ObjectList = new List<Save>();
}

 [System.Serializable]
public class Save
{
    public float object_id;
    public GameObject assetObject;
    public string assetName;
    public Vector3 objectPosition;
    public Vector3 objectRotation;
    public Vector3 objectScale;

    // Start is called before the first frame update



}

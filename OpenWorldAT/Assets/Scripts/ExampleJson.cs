using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ExampleJson : MonoBehaviour
{
    string path;
    string jsonString;

    // Start is called before the first frame update
    void Start()
    {
        path = Application.streamingAssetsPath + "/World.json";
        jsonString = File.ReadAllText(path);
        World Yumo = JsonUtility.FromJson<World>(jsonString);
        Debug.Log(Yumo.Name);
    }

    // Update is called once per frame

 [System.Serializable]
 public class World
    {
        public string Name;
        public int Level;
        public int[] Stats;
    }
}

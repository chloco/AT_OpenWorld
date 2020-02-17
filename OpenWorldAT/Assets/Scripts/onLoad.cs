using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class onLoad : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MapGenerator mapGen = new MapGenerator();
        mapGen.GenerateMap();
    }

    // Update is called once per frame
}

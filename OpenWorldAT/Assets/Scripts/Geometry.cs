using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Geometry : MonoBehaviour
{
    public static Geometry instance;
    public float object_id;
    public string assetName;
    public Vector3 objectPosition;
    public Vector3 objectRotation;
    public Vector3 objectScale;



    void Start()
    {
        object_id = transform.position.sqrMagnitude;
    }

    //private void Awake()
    //{
    //    if (instance == null)
    //    {
    //        instance = this;

    //    }
    //    else
    //    {
    //        if (instance != this)
    //        {
    //            Destroy(gameObject);

    //        }
    //    }

    //    DontDestroyOnLoad(gameObject);
    //}

    void Update()
    {
        assetName = this.gameObject.name;
        objectPosition = this.gameObject.transform.position;
        objectRotation = this.gameObject.transform.eulerAngles;
        objectScale = this.gameObject.transform.localScale;
        

   
    }
}
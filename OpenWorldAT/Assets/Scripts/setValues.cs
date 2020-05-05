using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setValues : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 position;
    public Vector3 rotation;
    public Vector3 scale;

    // Update is called once per frame
    void Update()
    {
        transform.position = position;
        transform.eulerAngles = rotation;
        transform.localScale = scale;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class onLoad : MonoBehaviour
{
    public string sceneName;
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene(sceneName, mode: LoadSceneMode.Additive);
    }

    // Update is called once per frame
}

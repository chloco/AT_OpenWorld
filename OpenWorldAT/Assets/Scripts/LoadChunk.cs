using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadChunk : MonoBehaviour
{
    public string sceneName;

    // Start is called before the first frame update

    // Update is called once per frame


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            SceneManager.LoadScene(sceneName, mode: LoadSceneMode.Additive);
            Debug.Log("entered");
        }
    }
}

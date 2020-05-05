using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;

public class SaveManager : MonoBehaviour
{
    private GameObject gObject;
    private Geometry the_cube;
    private LoadAssetBundles loadAsset;
    public static ClassCollector CC = new ClassCollector();

    void Start()
    {
        
        loadAsset = GameObject.FindGameObjectWithTag("Karen").GetComponent<LoadAssetBundles>();
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("Key Pressed");
            SaveButton();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("Key Pressed");
            LoadButton();
        }

    }

    private Save createSaveObject()
    {
        
        the_cube = FindObjectOfType<Geometry>();
        Save save = new Save();


        save.object_id = the_cube.object_id;
        save.assetObject = the_cube.assetObject;
        save.objectPosition = the_cube.objectPosition;
        save.objectRotation = the_cube.objectRotation;
        save.objectScale = the_cube.objectScale;
        save.assetName = the_cube.assetName;
        //CC.ObjectList.Add(save);

        return save;
    }

    public void SaveButton()
    {
        SaveJSON();
    }

    public void LoadButton()
    {
        LoadJSON();
    }

    public void SaveJSON()
    {
        
        the_cube = FindObjectOfType<Geometry>();
        Save save = new Save();


        save.object_id = the_cube.object_id;
        save.assetObject = the_cube.assetObject;
        save.objectPosition = the_cube.objectPosition;
        save.objectRotation = the_cube.objectRotation;
        save.objectScale = the_cube.objectScale;
        save.assetName = the_cube.assetName;
        CC.ObjectList.Add(save);

        string Json_String = JsonUtility.ToJson(CC);
        StreamWriter sw = new StreamWriter(Application.dataPath + "/JSONData.text");
        sw.Write(Json_String);
        sw.Close();

        Debug.Log("-=-=-=-SAVED-=-=-=-");
    }

    public void LoadJSON()
    {
        if (File.Exists(Application.dataPath + "/JSONData.text"))
        {
            //LOAD THE GAME
            StreamReader sr = new StreamReader(Application.dataPath + "/JSONData.text");

            string Json_String = sr.ReadToEnd();

            sr.Close();

            //Convert JSON to the Object(save)
            Save save = JsonUtility.FromJson<Save>(Json_String);//Into the Save Object

            Debug.Log("-=-=-=-LOADED-=-=-=-=-");

            //gObject = loadAsset.InstantiateObjectFromBundle(save.assetName).gameObject;
            
            foreach (Save save in CC.ObjectList)
            {

            }

            gObject = loadAsset.InstantiateObjectFromBundle2(save.assetName);
            gObject.transform.position = save.objectPosition;
            gObject.transform.eulerAngles = save.objectRotation;
            gObject.transform.localScale = save.objectScale;
            //MARKER LOAD THE DATA TO THE GAME
            //SaveGameManager.instance.something = save.something;
            ////SaveGameManager.instance.something_else = save.something_else;
            //SaveGameManager.instance.id = save.id;
            //SaveGameManager.instance.g_object = save.g_object;

            ////SaveGameManager.instance.Destroyy(gameObject); //how do i destroy the gameobject

            ////SaveGameManager.instance.coins = save.coinsNum;
            ////SaveGameManager.instance.diamonds = save.diamondsNum;

            //geomatary.instance.cube_id = save.id;
            //geomatary.instance.cube_positions = save.g_positions;
            //geomatary.instance.cube = save.g_object;

            //the_cube.transform.position = new Vector2(save.playerPositionX, save.playerPositionY);
            //player.transform.position = new Vector2(save.playerPositionX, save.playerPositionY);

            //MARKER Enemy position


        }


        else
        {
            Debug.Log("NOT FOUND FILE");
        }

    }

}
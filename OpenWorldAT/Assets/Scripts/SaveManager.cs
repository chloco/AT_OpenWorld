using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;

public class SaveManager : MonoBehaviour
{
    //[System.Serializable]
    //public class WorldData
    //{
    //    public int id;
    //    public 
    //    //public TerrainType[] regions;

    //}

    //public Bat batGameObject;

    //public SavePos saveGameObject;
    private Geometry the_cube;
 
    void Start()
    {
        the_cube = FindObjectOfType<Geometry>();
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
        Save save = new Save();

        //save.something = SaveGameManager.instance.something;
        //save.something_else = SaveGameManager.instance.something_else;
        //save.g_object = Save.instance.g_object;

        ////save.coinsNum = SaveGameManager.instance.coins;
        ////save.diamondsNum = SaveGameManager.instance.diamonds;
        //save.id = SaveGameManager.instance.id;
        //save.id = Geometry.instance.cube_id;
        //save.g_positions = Geometry.instance.cube_positions;
        save.playerPositionX = the_cube.transform.position.x;
        save.playerPositionY = the_cube.transform.position.y;
        save.g_object = Geometry.instance.cube;


        //save.playerPositionX = player.transform.position.x;
        //save.playerPositionY = player.transform.position.y;


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
        Save save = createSaveObject();
        string Json_String = JsonUtility.ToJson(save);
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

            the_cube.transform.position = new Vector2(save.playerPositionX, save.playerPositionY);
            //player.transform.position = new Vector2(save.playerPositionX, save.playerPositionY);

            //MARKER Enemy position


        }


        else
        {
            Debug.Log("NOT FOUND FILE");
        }

    }

}
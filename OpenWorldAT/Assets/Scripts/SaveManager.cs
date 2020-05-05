using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

public class SaveManager : MonoBehaviour
{
    
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
        save.objectPosition = the_cube.objectPosition;
        save.objectRotation = the_cube.objectRotation;
        save.objectScale = the_cube.objectScale;
        save.assetName = the_cube.assetName;
        CC.ObjectList.Add(save);

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

   
      
        var geometry = FindObjectsOfType(typeof(Geometry));
        Debug.Log(geometry + " : " + geometry.Length);
        //Save[] save = new Save[geometry.Length];
        
        foreach (Geometry obj in geometry)
        {
                 Save save = new Save();

                save.object_id = obj.object_id;
                save.objectPosition = obj.objectPosition;
                save.objectRotation =obj.objectRotation;
                save.objectScale = obj.objectScale;
                save.assetName = obj.assetName;

                CC.ObjectList.Add(save);  
            
             
        }

        //the_cube = FindObjectOfType<Geometry>();

        var jsonString = JsonConvert.SerializeObject(CC, Formatting.Indented);
        //string Json_String = JsonUtility.ToJson(CC);
        StreamWriter sw = new StreamWriter(Application.dataPath + "/JSONData.text");
        sw.Write(jsonString);
        sw.Close();

        Debug.Log("-=-=-=-SAVED-=-=-=-");
    }
  
    public class Item
    {
        public float object_id;
        public string assetName;
        public Vector3 objectPosition;
        public Vector3 objectRotation;
        public Vector3 objectScale;

    }
    public void LoadJSON()
    {
        if (File.Exists(Application.dataPath + "/JSONData.text"))
        {
            //LOAD THE GAME
            StreamReader sr = new StreamReader(Application.dataPath + "/JSONData.text");

            string Json_String = sr.ReadToEnd();
            Debug.Log(Json_String);
            ClassCollector classCollector;
            classCollector = JsonUtility.FromJson<ClassCollector>(Json_String);

            

            foreach(Save save in classCollector.ObjectList)
            {
                GameObject gameObj;
               string str =  Regex.Replace(save.assetName, @"\([^)]*\)", "");
                str = str.Replace(" ", "");
                //if (save.assetName.Contains("(Clone)"))
                //{
                //    str = save.assetName.Replace("(Clone)", "");
                //    gameObj = loadAsset.InstantiateObjectFromBundle2(str);
                //}
                //else
                //{
                gameObj = loadAsset.InstantiateObjectFromBundle2(str);
                //}
                
                gameObj.transform.position = save.objectPosition;
                gameObj.transform.eulerAngles = save.objectRotation;
                gameObj.transform.localScale = save.objectScale;
            }
            //List<Item> items = JsonConvert.DeserializeObject<List<Item>>(Json_String);



            //List<Save> save = JsonConvert.DeserializeObject<List<Save>>(Json_String);
            sr.Close();

            //Convert JSON to the Object(save)
            /*List<Save> save = JsonUtility.FromJson<Save>(Json_String);//Into the Save Objec*/
            
            
            Debug.Log("-=-=-=-LOADED-=-=-=-=-");

            //gObject = loadAsset.InstantiateObjectFromBundle(save.assetName).gameObject;

           
           //GameObject gObject;

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
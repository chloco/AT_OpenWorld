using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MapGenerator : MonoBehaviour
{
    public enum DrawMode { NoiseMap, ColourMap, Mesh}
    public DrawMode drawMode;
    public int mapWidth;
    public int mapHeight;
    public float noiseScale;
    public bool autoUpdate;
    public int octaves;
    public float meshHeightMultiplier;
    public AnimationCurve meshHeightCurve;
    
    public int seed;
    public Vector2 offset;
    [Range(0,1)]
    public float persistance;
    
    public float lacunarity;
    public TerrainType[] regions;

    string path;
    string path2;
    string jsonString;
    string jsonString2;

    int playerIndexX;
    int playerIndexY;

    [System.Serializable]
public class WorldData
    {
        public int MapWidth;
        public int MapHeight;
        public int Seed;
        public float NoiseScale;
        public int Octaves;
        public float Persistance;
        public float Lacunarity;
        public Vector2 Offset;
        //public TerrainType[] regions;

 }
    //public class colourRegions
    //{
    //    public TerrainType[] regions;
    //}

    public void GenerateMap() 
    {
        TerrainType[] regionColours = new TerrainType[8];
        regionColours[0].Height = 0.3f;
        regionColours[0].Colour = new Color(30.0f / 255f, 134.0f / 255f, 236.0f / 255f, 0.0f);
        regionColours[1].Height = 0.4f;
        regionColours[1].Colour = new Color(35.0f / 255f, 221.0f / 255f, 255.0f / 255f, 0.0f);
        regionColours[2].Height = 0.45f;
        regionColours[2].Colour = new Color(245.0f / 255f, 231.0f / 255f, 126.0f / 255f, 0.0f);
        regionColours[3].Height = 0.55f;
        regionColours[3].Colour = new Color(120.0f / 255f, 241.0f / 255f, 69.0f / 255f, 0.0f);
        regionColours[4].Height = 0.6f;
        regionColours[4].Colour = new Color(60.0f / 255f, 168.0f / 255f, 79.0f / 255f, 0.0f);
        regionColours[5].Height = 0.7f;
        regionColours[5].Colour = new Color(147.0f / 255f, 105.0f / 255f, 92.0f / 255f, 0.0f);
        regionColours[6].Height = 0.9f;
        regionColours[6].Colour = new Color(84.0f / 255f, 62.0f / 255f, 54.0f / 255f, 0.0f);
        regionColours[7].Height = 1.0f;
        regionColours[7].Colour = new Color(255.0f / 255f, 255.0f / 255f, 255.0f / 255f, 0.0f);

        path = Application.streamingAssetsPath + "/World.json";
        jsonString = File.ReadAllText(path);
        WorldData world = JsonUtility.FromJson<WorldData>(jsonString);


        //path2 = Application.streamingAssetsPath + "/colourRegions.json";
        //jsonString2 = File.ReadAllText(path2);
        //colourRegions colours = JsonUtility.FromJson<colourRegions>(jsonString2);

        //string json = JsonUtility.ToJson(colours.regions[1]);
        //Debug.Log(colours);
        mapWidth = world.MapWidth;
        mapHeight = world.MapHeight;
        float[,] noiseMap = Noise.GenerateNoiseMap(world.MapWidth, world.MapHeight, world.Seed, world.NoiseScale, world.Octaves, world.Persistance, world.Lacunarity, world.Offset);
        //float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, seed, noiseScale, octaves, persistance, lacunarity, offset);
        

        Color[] colourMap = new Color[world.MapHeight * world.MapWidth];
        for (int y = 0; y < world.MapHeight; y++)
        {
            for (int x = 0; x < world.MapWidth; x++)
            {
                float currentHeight = noiseMap[x, y];
                for (int i = 0; i < regionColours.Length; i++)
                {
                    if (currentHeight <= regionColours[i].Height)
                    {
                        colourMap[y * world.MapWidth + x] = regionColours[i].Colour;
                        break;
                    }
                }
            }
        }



        SaveTextureAsPNG(TextureGenerator.TextureFromHeightMap(noiseMap), "world.png");

        LoadTexture();
        //Color[] colourMap = new Color[mapHeight * mapWidth];
        //for (int y = 0; y < mapHeight; y++)
        //{
        //    for (int x = 0; x < mapWidth; x++)
        //    {
        //        float currentHeight = noiseMap[x, y];
        //        for (int i = 0; i < regions.Length; i++)
        //        {
        //            if (currentHeight <= regions[i].height)
        //            {
        //                colourMap[y * mapWidth + x] = regions[i].colour;
        //                break;
        //            }
        //        }
        //    }
        //}
        playerIndexX = 3;
        playerIndexY = 2;
        MapDisplay display = FindObjectOfType<MapDisplay>();
        MeshGenerator meshGenerator = new MeshGenerator();
        int y_value = -4;
        int x_value = 4;
        int z_value = 2;
        //display.DrawMesh(meshGenerator.GenerateTerrainChunk(noiseMap, 0, 0), TextureGenerator.TextureFromColourMap(colourMap, world.MapWidth, world.MapHeight), 0, ((0 * (world.MapWidth / 4)) /*- (x_value * 0)*/), ((0 * (world.MapHeight / 4)) - (y_value * 0)));
        //display.DrawMesh(meshGenerator.GenerateTerrainChunk(noiseMap, 0, 1), TextureGenerator.TextureFromColourMap(colourMap, world.MapWidth, world.MapHeight), 0, ((1 * (world.MapWidth / 4)) /*- (x_value * 1)*/), ((0 * (world.MapHeight / 4)) - (y_value * 2)));
        //display.DrawMesh(meshGenerator.GenerateTerrainChunk(noiseMap, 0, 2), TextureGenerator.TextureFromColourMap(colourMap, world.MapWidth, world.MapHeight), 0, ((2 * (world.MapWidth / 4)) /*- (x_value * 2)*/), ((0 * (world.MapHeight / 4)) - (y_value * 3)));
        //display.DrawMesh(meshGenerator.GenerateTerrainChunk(noiseMap, 0, 3), TextureGenerator.TextureFromColourMap(colourMap, world.MapWidth, world.MapHeight), 0, ((3 * (world.MapWidth / 4)) - (x_value * 3)), ((0 * (world.MapHeight / 4)) - (y_value * 4)));

        //display.DrawMesh(meshGenerator.GenerateTerrainChunk(noiseMap, 0, 0), TextureGenerator.TextureFromColourMap(colourMap, world.MapWidth, world.MapHeight), 0 * world.MapWidth, ((0 * (world.MapHeight / 4)) - (y_value * 0)), 0);
        //display.DrawMesh(meshGenerator.GenerateTerrainChunk(noiseMap, 0, 1), TextureGenerator.TextureFromColourMap(colourMap, world.MapWidth, world.MapHeight), 1 * world.MapWidth, ((0 * (world.MapHeight / 4)) - (y_value * 0)), 0);
        //display.DrawMesh(meshGenerator.GenerateTerrainChunk(noiseMap, 0, 2), TextureGenerator.TextureFromColourMap(colourMap, world.MapWidth, world.MapHeight), 2 * world.MapWidth, ((0 * (world.MapHeight / 4)) - (y_value * 0)), 0);
        //display.DrawMesh(meshGenerator.GenerateTerrainChunk(noiseMap, 0, 3), TextureGenerator.TextureFromColourMap(colourMap, world.MapWidth, world.MapHeight), 3 * world.MapWidth, ((0 * (world.MapHeight / 4)) - (y_value * 0)), 0);
        //display.DrawMesh(meshGenerator.GenerateTerrainChunk(noiseMap, 1, 0), TextureGenerator.TextureFromColourMap(colourMap, world.MapWidth, world.MapHeight), 0 * world.MapWidth, ((0 * (world.MapHeight / 4)) - (y_value * 1)), 0);
        //display.DrawMesh(meshGenerator.GenerateTerrainChunk(noiseMap, 1, 1), TextureGenerator.TextureFromColourMap(colourMap, world.MapWidth, world.MapHeight), 1 * world.MapWidth, ((0 * (world.MapHeight / 4)) - (y_value * 1)), 0);
        //display.DrawMesh(meshGenerator.GenerateTerrainChunk(noiseMap, 1, 2), TextureGenerator.TextureFromColourMap(colourMap, world.MapWidth, world.MapHeight), 2 * world.MapWidth, ((0 * (world.MapHeight / 4)) - (y_value * 1)), 0);
        //display.DrawMesh(meshGenerator.GenerateTerrainChunk(noiseMap, 1, 3), TextureGenerator.TextureFromColourMap(colourMap, world.MapWidth, world.MapHeight), 3 * world.MapWidth, ((0 * (world.MapHeight / 4)) - (y_value * 1)), 0);


        display.DrawMesh(meshGenerator.GenerateTerrainChunk(noiseMap, 0, 0), TextureGenerator.TextureFromColourMap(colourMap, world.MapWidth, world.MapHeight), -x_value * 0, 0, 0);
        display.DrawMesh(meshGenerator.GenerateTerrainChunk(noiseMap, 0, 1), TextureGenerator.TextureFromColourMap(colourMap, world.MapWidth, world.MapHeight), -x_value * 1, 0, 0);
        display.DrawMesh(meshGenerator.GenerateTerrainChunk(noiseMap, 0, 2), TextureGenerator.TextureFromColourMap(colourMap, world.MapWidth, world.MapHeight), -x_value * 2, 0, 0);
        display.DrawMesh(meshGenerator.GenerateTerrainChunk(noiseMap, 0, 3), TextureGenerator.TextureFromColourMap(colourMap, world.MapWidth, world.MapHeight), -x_value * 3, 0, 0);
        display.DrawMesh(meshGenerator.GenerateTerrainChunk(noiseMap, 1, 0), TextureGenerator.TextureFromColourMap(colourMap, world.MapWidth, world.MapHeight), -x_value * 0, z_value, -y_value);
        display.DrawMesh(meshGenerator.GenerateTerrainChunk(noiseMap, 1, 1), TextureGenerator.TextureFromColourMap(colourMap, world.MapWidth, world.MapHeight), -x_value * 1, z_value, -y_value);
        display.DrawMesh(meshGenerator.GenerateTerrainChunk(noiseMap, 1, 2), TextureGenerator.TextureFromColourMap(colourMap, world.MapWidth, world.MapHeight), -x_value * 2, z_value, -y_value);
        display.DrawMesh(meshGenerator.GenerateTerrainChunk(noiseMap, 1, 3), TextureGenerator.TextureFromColourMap(colourMap, world.MapWidth, world.MapHeight), -x_value * 3, z_value, -y_value);

        if (drawMode == DrawMode.NoiseMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromHeightMap(noiseMap));
        }
        else if (drawMode == DrawMode.ColourMap)
        {
            //display.DrawTexture(TextureGenerator.TextureFromColourMap(colourMap, world.MapWidth, world.MapHeight)); 
        }
        else if (drawMode == DrawMode.Mesh)
        {
            //display.DrawMesh(meshGenerator.GenerateTerrainChunk(noiseMap, 3,2), TextureGenerator.TextureFromColourMap(colourMap, world.MapWidth, world.MapHeight), playerIndexX, playerIndexY);
        }
    }
    void Update()
    {
        

    }
    IEnumerator LoadTexture()
    {
        yield return new WaitForSeconds(1.5f);
        Texture2D test = new Texture2D(mapWidth, mapHeight);
        byte[] data = File.ReadAllBytes("world.png");
        test.LoadImage(data);
    }
    public static void SaveTextureAsPNG(Texture2D _texture, string _fullPath)
    {
        byte[] _bytes = _texture.EncodeToPNG();
        var dirPath = Application.streamingAssetsPath;
        System.IO.File.WriteAllBytes(dirPath + "/" + _fullPath, _bytes);
        Debug.Log(_bytes.Length / 1024 + "Kb was saved as: " + _fullPath);
    }
    private void OnValidate()
    {
        if (mapWidth < 1)
        {
            mapWidth = 1;
        }
        if(mapHeight < 1 )
        {
            mapWidth = 1;
        }
        if(lacunarity < 1)
        {
            lacunarity = 1;
        }
        if(octaves < 0 )
        {
            octaves = 0;
        }
    }
}

[System.Serializable]
public struct TerrainType
{
    public float Height;
    public Color Colour;
    
    
}
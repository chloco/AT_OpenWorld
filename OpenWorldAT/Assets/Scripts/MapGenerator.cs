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
    string jsonString;

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

    public void GenerateMap()
    {
        path = Application.streamingAssetsPath + "/World.json";
        jsonString = File.ReadAllText(path);
        WorldData world = JsonUtility.FromJson<WorldData>(jsonString);

        float[,] noiseMap = Noise.GenerateNoiseMap(world.MapWidth, world.MapHeight, world.Seed, world.NoiseScale, world.Octaves, world.Persistance, world.Lacunarity, world.Offset);
        //float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, seed, noiseScale, octaves, persistance, lacunarity, offset);


        Color[] colourMap = new Color[world.MapHeight * world.MapWidth];
        for (int y = 0; y < world.MapHeight; y++)
        {
            for (int x = 0; x < world.MapWidth; x++)
            {
                float currentHeight = noiseMap[x, y];
                for (int i = 0; i < regions.Length; i++)
                {
                    if (currentHeight <= regions[i].height)
                    {
                        colourMap[y * world.MapWidth + x] = regions[i].colour;
                        break;
                    }
                }
            }
        }

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
        SaveTextureAsPNG(TextureGenerator.TextureFromHeightMap(noiseMap), "world.png");


        MapDisplay display = FindObjectOfType<MapDisplay>();
        if(drawMode == DrawMode.NoiseMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromHeightMap(noiseMap));
        }
        else if(drawMode == DrawMode.ColourMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromColourMap(colourMap, world.MapWidth, world.MapHeight));
        }
        else if (drawMode == DrawMode.Mesh)
        {
            display.DrawMesh(MeshGenerator.GenerateTerrainMesh(noiseMap, meshHeightMultiplier, meshHeightCurve), TextureGenerator.TextureFromColourMap(colourMap, world.MapWidth, world.MapHeight));
        }
        
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
    public string name;
    public float height;
    public Color colour;
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public  class MeshGenerator
{
    

  public Chunk GenerateTerrainChunk(float[,] heightMap, int chunkIndexY, int chunkIndexX)
    {
        int width = heightMap.GetLength(0);
        int height = heightMap.GetLength(1);
        float heightMultiplier = 25;
        //getting the centre of the screen
        float topLeftX = (width - 1) / -2f;
        float topLeftZ = (height - 1) / 2f;
        int vertexIndex = 0;
        int noOfChunks = 4;
        int chunkWidth = width/noOfChunks;
        int chunkHeight = height/noOfChunks;

        Chunk chunk = new Chunk(chunkWidth, chunkHeight);
        for(int y = chunkIndexY * chunkHeight; y<(chunkIndexY + 1) * chunkHeight && y < height; y++)
        {
            for (int x = chunkIndexX * chunkWidth; x < (chunkIndexX + 1) * chunkWidth && x < width; x++)
            {
                chunk.vertices[vertexIndex] = new Vector3(topLeftX + x, heightMap[x, y] * heightMultiplier, topLeftZ -  y);
                chunk.uvs[vertexIndex] = new Vector2(x / (float)width, y / (float)height);
                if(x< (chunkIndexX + 1) * chunkWidth - 1 && y< (chunkIndexY + 1) * chunkHeight - 1)
                {
                    chunk.AddTriangle(vertexIndex, vertexIndex + chunkWidth + 1, vertexIndex + chunkWidth);
                    chunk.AddTriangle(vertexIndex + chunkWidth + 1, vertexIndex, vertexIndex + 1);
                }
                       
                vertexIndex++;
   
            }
        }
        string path = Application.streamingAssetsPath + "/chunk" + chunkIndexX + chunkIndexY + ".json";
        string jsonString = JsonUtility.ToJson(chunk, true);

        File.WriteAllText(path, jsonString);         

        return chunk;
    }
}


public class MeshData
{
    public Vector3[] vertices;
    public int[] triangles;
    public Vector2[] uvs;
    int triangleIndex;


    public MeshData(int meshWidth, int meshHeight)
    { 
        vertices = new Vector3[meshWidth * meshHeight];
        uvs = new Vector2[meshWidth * meshHeight];
        triangles = new int[(meshWidth - 1)*(meshHeight-1)*6];
 
    }

    //public void AddTriangle(int a, int b, int c)
    //{
    //    triangles[triangleIndex] = a;
    //    triangles[triangleIndex+1] = b;
    //    triangles[triangleIndex+2] = c;
    //    triangleIndex += 3;
    //}

    //public Mesh CreateMesh()
    //{
    //    Mesh mesh = new Mesh();
    //    mesh.vertices = vertices;
    //    mesh.triangles = triangles;
    //    mesh.uv = uvs;
    //    mesh.RecalculateNormals();

    //    return mesh;
    //}
}

[System.Serializable]
public  class Chunk
{
    public Vector3[] vertices;
    public Vector2[] uvs;
    public int triangleIndex;
    public int size;
    public Vector4[] tangents;
    public int[] triangles;
    public Vector2[] offset;

    public Chunk(int chunkWidth, int chunkHeight)
    {
        vertices = new Vector3[chunkWidth * chunkHeight];
        uvs = new Vector2[chunkWidth * chunkHeight];
        triangles = new int[(chunkWidth - 1) * (chunkHeight - 1) * 6];

    }

    public void AddTriangle(int a, int b, int c)
    {
        if(triangleIndex > 14403)
        {
            bool boo = false;
        }
        else
        {
            triangles[triangleIndex] = a;
            triangles[triangleIndex + 1] = b;
            triangles[triangleIndex + 2] = c;
            triangleIndex += 3;
        }
        
    }

    public Mesh CreateMesh()
    {
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();

        return mesh;
    }


}
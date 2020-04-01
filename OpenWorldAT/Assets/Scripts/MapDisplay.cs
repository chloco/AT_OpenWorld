using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDisplay : MonoBehaviour
{
    //public Renderer textureRenderer;
    //public MeshFilter meshFilter;
    //public MeshRenderer meshRenderer;
    public GameObject mesh;

    public void DrawTexture(Texture2D texture)
    {
        //textureRenderer.sharedMaterial.mainTexture = texture;
        //textureRenderer.transform.localScale = new Vector3(texture.width, 1, texture.height);
    }

    public void DrawMesh(Chunk chunkData, Texture2D texture, int indexX, int indexZ, int indexY)
    {
        GameObject plane = new GameObject("plane");
        plane.transform.localScale = new Vector3(10, 10, 10);
        plane.transform.position = mesh.transform.position + new Vector3(indexX, indexZ, indexY);

        MeshFilter meshFilter = (MeshFilter)plane.AddComponent(typeof(MeshFilter));
        MeshRenderer meshRenderer = plane.AddComponent<MeshRenderer>();
        meshFilter.sharedMesh = chunkData.CreateMesh();

        meshRenderer.material.mainTexture = texture;
        MeshCollider meshc = plane.AddComponent(typeof(MeshCollider)) as MeshCollider;
        meshc.sharedMesh = chunkData.CreateMesh();

        //meshFilter.sharedMesh = chunkData.CreateMesh();
        //meshRenderer.sharedMaterial.mainTexture = texture;
        //MeshCollider meshc = mesh.AddComponent(typeof(MeshCollider)) as MeshCollider;
        //meshc.sharedMesh = chunkData.CreateMesh();
    }
}

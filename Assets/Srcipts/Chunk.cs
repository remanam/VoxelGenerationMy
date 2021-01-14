using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;


    List<Vector3> vertices = new List<Vector3>();
    List<int> triangles = new List<int>();
    List<Vector2> uvs = new List<Vector2>();



    Mesh mesh;
    
    Vector3 offset;

    void Start()
    {
        mesh = gameObject.GetComponent<MeshFilter>().mesh;
        meshRenderer = gameObject.GetComponent<MeshRenderer>();


        AddVoxelDataToChunk(new Vector3(0,0,0));

        /*        for (int x = 0; x < VoxelData.ChunkWidth; x++) {
                    for (int y = 0; y < VoxelData.ChunkHeight; y++) {
                        for (int z = 0; z < VoxelData.ChunkWidth; z++) {
                            AddVoxelDataToChunk(new Vector3(x,y,z));

                        }
                    }
                }*/
        CreateMesh();
    }

    void AddVoxelDataToChunk(Vector3 pos)
    {
        
        int currentIndex = 0;
        // Кубик
        for (int p = 0; p < 6; p++) {
            // Используем точки из текущего полигона p
            // То есть тут взяли 4 точки для треугольников стороны p
            vertices.Add(pos + VoxelData.vertices[VoxelData.triangles[p, 0]]);
            vertices.Add(pos + VoxelData.vertices[VoxelData.triangles[p, 1]]);
            vertices.Add(pos + VoxelData.vertices[VoxelData.triangles[p, 2]]);
            vertices.Add(pos + VoxelData.vertices[VoxelData.triangles[p, 3]]);



            // Текстурируем Mesh
            AddTexture();

            triangles.Add(currentIndex    );
            triangles.Add(currentIndex + 1);
            triangles.Add(currentIndex + 2);

            triangles.Add(currentIndex + 2);
            triangles.Add(currentIndex + 1);
            triangles.Add(currentIndex + 3);

            // Это, чтобы следующие 4 точки брались в набор
            currentIndex += 4;

            

        }      
            
    }

    void AddTexture()
    {
        uvs.Add(VoxelData.uv[0]);
        uvs.Add(VoxelData.uv[1]);
        uvs.Add(VoxelData.uv[2]);
        uvs.Add(VoxelData.uv[3]);
    }
    private void CreateMesh()
    {
        Mesh mesh = new Mesh();

        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.uv = uvs.ToArray();
        
        mesh.RecalculateNormals();
        meshFilter.mesh = mesh;
    }
}

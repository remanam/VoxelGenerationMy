using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VoxelData
{
    public static readonly int ChunkWidth = 5;
    public static readonly int ChunkHeight = 5;

    public static readonly Vector3[] vertices = new Vector3[8] {
       new Vector3(0.0f, 0.0f, 0.0f),  //3 2
       new Vector3(1.0f, 0.0f, 0.0f ), //0 1
       new Vector3(1.0f, 1.0f, 0.0f ),
       new Vector3(0.0f, 1.0f, 0.0f ),

       new Vector3(0.0f, 0.0f,  1.0f),
       new Vector3(1.0f, 0.0f, 1.0f ),
       new Vector3(1.0f, 1.0f, 1.0f ),
       new Vector3(0.0f, 1.0f, 1.0f ),

    };
    public static readonly int[ , ] triangles = new int[6, 4]{ 
        {0, 3, 1, 2 }, // front face
        {5, 6, 4, 7 }, // back face
        {3, 7, 2, 6 }, // Top face
        {4, 0, 5, 1 }, // Bottom face
        {4, 7, 0, 3 }, // Left face
        {1, 2, 5, 6 }, // Right face
        
    };

    public static readonly Vector2[] uv = new Vector2[4] {
        // В каком порядке в Triangles используются точку в таком и подаём uv
        new Vector2(0.0f, 0.0f),
        new Vector2(0.0f, 1.0f),
        new Vector2(1.0f, 0.0f),
        new Vector2(1.0f, 1.0f),
    };
}

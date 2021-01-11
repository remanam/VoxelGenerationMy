using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VoxelData
{
    public static readonly int ChunkWidth;
    public static readonly int ChunkHeight;

    public static readonly Vector3[] vertices = new Vector3[8] {
       new Vector3(0.0f, 0.0f, 0.0f), 
       new Vector3(1.0f, 0.0f, 0.0f ),
       new Vector3(0.0f, 1.0f, 0.0f ),
       new Vector3(1.0f, 1.0f, 0.0f ),

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
}

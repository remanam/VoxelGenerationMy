using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VoxelData
{
    public static readonly int ChunkWidth = 5;
    public static readonly int ChunkHeight = 15;

    public static readonly int AtlasSizeInBlocks = 16; // Texture atlas 16x16
    public static float NormalizedTextureSize {
        get { return 1f / AtlasSizeInBlocks;  } // Ёто длина или ширина 1 блока, относительно атласа
    }


    public static readonly Vector3[] vertices = new Vector3[8] {
       new Vector3(0.0f, 0.0f, 0.0f),  //3 2
       new Vector3(1.0f, 0.0f, 0.0f ), //0 1
       new Vector3(1.0f, 1.0f, 0.0f ),
       new Vector3(0.0f, 1.0f, 0.0f ),

       new Vector3(0.0f, 0.0f, 1.0f ),
       new Vector3(1.0f, 0.0f, 1.0f ),
       new Vector3(1.0f, 1.0f, 1.0f ),
       new Vector3(0.0f, 1.0f, 1.0f ),

    };

    // this array check a polygon in front of each face
    // need this to check if i have a voxel outside
    public static readonly Vector3[] faceChecks = new Vector3[6] {
        new Vector3(0.0f, 0.0f, -1.0f), // провер€ем, есть ли спереди кубика что то
        new Vector3(0.0f, 0.0f, 1.0f), // есть ли сзади
        new Vector3(0.0f, 1.0f, 0.0f), // есть ли сверху
        new Vector3(0.0f, -1.0f, 0.0f), // есть ли снизу
        new Vector3(-1.0f, 0.0f, 0.0f), // есть ли слева
        new Vector3(1.0f, 0.0f, 0.0f), // есть ли справа
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
        // ¬ каком пор€дке в Triangles используютс€ точку в таком и подаЄм uv
        new Vector2(0.0f, 0.0f),
        new Vector2(0.0f, 1.0f),
        new Vector2(1.0f, 0.0f),
        new Vector2(1.0f, 1.0f),
    };
}

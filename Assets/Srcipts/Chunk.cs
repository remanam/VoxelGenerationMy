using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;
    Mesh mesh;


    List<Vector3> vertices = new List<Vector3>();
    List<int> triangles = new List<int>();
    List<Vector2> uvs = new List<Vector2>();

    int currentIndex = 0;

    public BlockType[] blockTypes; // Массив типов блоков


    byte[,,] voxelMap = new byte[VoxelData.ChunkWidth, VoxelData.ChunkHeight, VoxelData.ChunkWidth];

    Vector3 offset;

    void Start()
    {
        mesh = gameObject.GetComponent<MeshFilter>().mesh;
        meshFilter = gameObject.GetComponent<MeshFilter>();
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        


        int i = 0;

        PopulateVoxelmap();
        CreateChunkMesh();
        CreateMesh();

    }

    void PopulateVoxelmap()
    {
        for (int x = 0; x < VoxelData.ChunkWidth; x++) {
            for (int y = 0; y < VoxelData.ChunkHeight; y++) {
                for (int z = 0; z < VoxelData.ChunkWidth; z++) {

                    voxelMap[x, y, z] = GetVoxel(new Vector3(x,y,z));

                }
            }
        }
    }

    void CreateChunkMesh()
    {
        for (int x = 0; x < VoxelData.ChunkWidth; x++) {
            for (int y = 0; y < VoxelData.ChunkHeight; y++) {
                for (int z = 0; z < VoxelData.ChunkWidth; z++) {
                  
                        AddVoxelDataToChunk(new Vector3(x, y, z));

                }
            }
        }
    }

    void AddVoxelDataToChunk(Vector3 pos)
    {
           
        // Кубик
        for (int p = 0; p < 6; p++) {

            // если на расстоянии координаты грани нет твердого блока, то рисуем
            // То есть рисуем только крайнии блоки
            if (CheckVoxel(pos + VoxelData.faceChecks[p]) == false) {

                // Используем точки из текущего полигона p
                // То есть тут взяли 4 точки для треугольников стороны p
                vertices.Add( VoxelData.vertices[VoxelData.triangles[p, 0]] + pos );
                vertices.Add( VoxelData.vertices[VoxelData.triangles[p, 1]] + pos );
                vertices.Add( VoxelData.vertices[VoxelData.triangles[p, 2]] + pos );
                vertices.Add( VoxelData.vertices[VoxelData.triangles[p, 3]] + pos );        

                // Текстурируем Mesh
                AddTexture(blockTypes[1].GetTextureID(p)); // Получаем каждую грань блока 

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
    }

    

    bool IsVoxelInChunk(Vector3 pos)
    {
        // pos.x строго больше, поэтому VoxelData.ChunkWidth - 1
        if (pos.x < 0 || pos.x > VoxelData.ChunkWidth  - 1 ||
            pos.y < 0 || pos.y > VoxelData.ChunkHeight - 1  ||
            pos.z < 0 || pos.z > VoxelData.ChunkWidth  - 1 )
            return false;
        else
            return true;
    }

    bool CheckVoxel(Vector3 pos)
    {
        int x = Mathf.FloorToInt(pos.x);
        int y = Mathf.FloorToInt(pos.y);
        int z = Mathf.FloorToInt(pos.z);

        // Проверка находится ли воксель за пределами нашего чанка
        if (IsVoxelInChunk(new Vector3(x,y,z)) == false)
            return false;
            

        return blockTypes[voxelMap[x,y,z]].isSolid;
    }


    void AddTexture(int textureID)
    {
        //Всё равно непонятно как эта дичь работает
        float y = textureID / VoxelData.AtlasSizeInBlocks; // Получаем высоту, например 8/16,
        

        // Теперь нужно правильно прибавить координату Х, чтобы переходы по высоте был
        float x = textureID - (y * VoxelData.AtlasSizeInBlocks); // Например ID = 5.  x = 5 - 5/16 * 16

         

        x *= VoxelData.NormalizedTextureSize;
        y *= VoxelData.NormalizedTextureSize;

        // normalized вычитаем, так как отсчёт снизу начинается от 0
        y = 1f - y - VoxelData.NormalizedTextureSize;

        Debug.Log(new Vector2(x, y));
        Debug.Log(new Vector2(x, y + VoxelData.NormalizedTextureSize));
        Debug.Log(new Vector2(x + VoxelData.NormalizedTextureSize, y));
        Debug.Log(new Vector2(x + VoxelData.NormalizedTextureSize, y + VoxelData.NormalizedTextureSize));

        


        uvs.Add(new Vector2(x,y));
        uvs.Add(new Vector2(x, y + VoxelData.NormalizedTextureSize));
        uvs.Add(new Vector2(x + VoxelData.NormalizedTextureSize, y));
        uvs.Add(new Vector2(x + VoxelData.NormalizedTextureSize, y + VoxelData.NormalizedTextureSize));

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

    //Возвращает тип блока
    public byte GetVoxel(Vector3 pos)
    {
        return 1; // Камень
    }
}


[System.Serializable]
public class BlockType
{
    public string blockName;
    public bool isSolid;

    [Header("Texture values")]
    public int backFaceTexture;
    public int frontFaceTexture;
    public int topFaceTexture;
    public int bottomFaceTexture;
    public int leftFaceTexture;
    public int rightFaceTexture;

    public int GetTextureID(int faceIndex)
    {
        switch (faceIndex) {
            case 0:
                return frontFaceTexture; 
            case 1:
                return backFaceTexture;
            case 2:
                return topFaceTexture;
            case 3:
                return bottomFaceTexture;
            case 4:
                return leftFaceTexture;
            case 5:
                return rightFaceTexture;
            default:
                Debug.Log("GetTexture : whong texture faceID");
                return 0;
        }
    }
}

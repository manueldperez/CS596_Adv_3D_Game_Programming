using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[ExecuteInEditMode]

public class PGC_Terrain : MonoBehaviour
{

    Mesh mesh;

    Vector3[] vertices;
    int[] triangles;

    private int xSize = 50;
    private int zSize = 50;

    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        PerlinNoise();
        UpdateMesh();
    }

    void PerlinNoise()
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];
        float scale = Random.Range(10f, 15f);
        float height = Random.Range(2f, 9f);

        for (int z = 0, i = 0; z <= zSize; z++)
        {
            for(int x = 0; x <= xSize; x++)
            {
                float xCoord = (float)x / xSize * scale;
                float yCoord = (float)z / zSize * scale;

                float y = Mathf.PerlinNoise(xCoord, yCoord) * height;
                vertices[i] = new Vector3(x, y, z);
                i++;
            }
        }

        triangles = new int[xSize * zSize * 6];

        int vert = 0;
        int tris = 0;

        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;

                vert++;
                tris += 6;
            }
            vert++;
        }

    }

    //void CreateShape()
    //{
    //    vertices = new Vector3[(xSize + 1) * (zSize + 1)];

    //    for (int z = 0, i = 0; z <= zSize; z++)
    //    {
    //        for (int x = 0; x <= xSize; x++)
    //        {
    //            float y = Mathf.PerlinNoise(x * 0.5f, z * 0.5f) * 2f;
    //            vertices[i] = new Vector3(x, y, z);
    //            i++;
    //        }
    //    }

    //    triangles = new int[xSize * zSize * 6];

    //    int vert = 0;
    //    int tris = 0;

    //    for (int z = 0; z < zSize; z++)
    //    {
    //        for (int x = 0; x < xSize; x++)
    //        {
    //            triangles[tris + 0] = vert + 0;
    //            triangles[tris + 1] = vert + xSize + 1;
    //            triangles[tris + 2] = vert + 1;
    //            triangles[tris + 3] = vert + 1;
    //            triangles[tris + 4] = vert + xSize + 1;
    //            triangles[tris + 5] = vert + xSize + 2;

    //            vert++;
    //            tris += 6;
    //        }
    //        vert++;
    //    }

    //}

    // Update is called once per frame
    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }

    private void OnDrawGizmos()
    {
        if (vertices == null)
        {
            return;
        }

        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], .1f);
        }
    }

}

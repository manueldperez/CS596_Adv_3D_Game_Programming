using UnityEngine;

public class PGC_Mesh : MonoBehaviour
{

    public int xSize, ySize;

    private Mesh mesh;
    private Vector3[] vertices;
    private int[] triangles;
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    private BoxCollider collider;
    private GameObject cube;

    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        meshFilter = gameObject.AddComponent<MeshFilter>();
        GetComponent<MeshFilter>().mesh = mesh;

        meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshRenderer.material = Resources.Load<Material>("Material/water");

        collider = gameObject.AddComponent<BoxCollider>();
        collider.size = new Vector3(10, 0, 10);

        cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.AddComponent<Rigidbody>();
        cube.transform.position = new Vector3(5, 5, 5);

    }

    private void Update()
    {
        vertices = new Vector3[(xSize + 1) * (ySize + 1)];

        for (int i = 0, y = 0; y <= ySize; y++)
        {
            for (int x = 0; x <= xSize; x++, i++)
            {
                vertices[i] = new Vector3(x, 0.5f * Mathf.Sin(Time.time + i), y);
            }
        }

        triangles = new int[xSize * ySize * 6];

        int ver = 0;
        int tri = 0;

        for (int y = 0; y < ySize; y++)
        {
            for (int x = 0; x < xSize; x++)
            {

                triangles[tri + 0] = ver + 0;
                triangles[tri + 1] = ver + xSize + 1;
                triangles[tri + 2] = ver + 1;

                triangles[tri + 3] = ver + 1;
                triangles[tri + 4] = ver + xSize + 1;
                triangles[tri + 5] = ver + xSize + 2;

                ver++;
                tri += 6;
            }
            ver++;
        }

        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }
}
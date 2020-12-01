using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]

public class Perlin_Terrain : MonoBehaviour
{
    public Terrain terrain;
    public TerrainData terrainData;

    private void Start()
    {
        int perl_or_multPerl = Random.Range(0, 2);
        if(perl_or_multPerl == 1)
        {
            PerlinNoise();
        }
        else
        {
            MultiplePerlin();
        }
        
    }

    private void OnEnable()
    {
        terrain = GetComponent<Terrain>();
        terrainData = Terrain.activeTerrain.terrainData;

        terrainData.size = new Vector3(2000f, 600f, 2000f);
    }

    public void PerlinNoise()
    {
        int xRes = terrainData.heightmapWidth;
        int yRes = terrainData.heightmapHeight;

        //Rotates the terrain in a downhill manner
        float[,] heights = new float[yRes, xRes];
        for (int x = 0; x < xRes; x++)
        {
            for (int y = 0; y < yRes; y++)
            {
                heights[y, x] = (float)x / xRes;
            }
        }
        terrainData.SetHeights(0, 0, heights);

        //float scale = Random.Range(10f, 15f);
        float height = Random.Range(2f, 9f);

        heights = terrainData.GetHeights(0, 0, xRes, yRes);

        for (int j = 0; j < yRes; j++)
        {
            for (int i = 0; i < xRes; i++)
            {
                float xCoord = (float)i / xRes * 15f;
                float yCoord = (float)j / yRes * 15f;

                heights[i, j] += Mathf.PerlinNoise(xCoord * 0.9f, yCoord * 0.8f) * 0.3f;
            }
        }

        terrainData.SetHeights(0, 0, heights);
    }

    public void MultiplePerlin()
    {
        float mPerlinHeightScale = Random.Range(0.2f, 0.38f);

        int xRes = terrainData.heightmapWidth;
        int yRes = terrainData.heightmapHeight;

        //Rotates the terrain in a downhill manner
        float[,] heights = new float[yRes, xRes];
        for (int x = 0; x < xRes; x++)
        {
            for (int y = 0; y < yRes; y++)
            {
                heights[y, x] = (float)x / xRes;
            }
        }
        terrainData.SetHeights(0, 0, heights);

        heights = terrainData.GetHeights(0, 0, xRes, yRes);

        for (int j = 0; j < yRes; j++)
        {
            for (int i = 0; i < xRes; i++)
            {
                foreach (PerlinParameters p in perlinParameters)
                {
                    heights[i, j] += fBM((i + p.mPerlinOffsetX) * p.mPerlinXScale,
                                (j + p.mPerlinOffsetY) * p.mPerlinYScale,
                                p.mPerlinOctaves,
                                p.mPerlinPersistance) * mPerlinHeightScale;
                }
            }
        }
        terrainData.SetHeights(0, 0, heights);
    }

    public class PerlinParameters
    {
        public float mPerlinXScale = 0.01f;
        public float mPerlinYScale = 0.01f;

        public int mPerlinOctaves = 3;
        public float mPerlinPersistance = 6;

        public int mPerlinOffsetX = 0;
        public int mPerlinOffsetY = 0;
        public bool remove = false;
    }

    public List<PerlinParameters> perlinParameters = new List<PerlinParameters>()
    {
        new PerlinParameters()
    };

    public float fBM(float x, float y, int oct, float persistance)
    {
        float total = 0;
        float frequency = 1;
        float amplitude = 1;
        float maxValue = 0.01f;
        for (int i = 0; i < oct; i++)
        {
            total += Mathf.PerlinNoise(x * frequency, y * frequency) * amplitude;
            maxValue += amplitude;
            amplitude *= persistance;
            frequency *= 2;
        }

        return total / maxValue;
    }
    
}
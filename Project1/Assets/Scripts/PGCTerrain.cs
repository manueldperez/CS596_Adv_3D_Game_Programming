using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]

public class PGCTerrain : MonoBehaviour
{
    public Terrain terrain;
    public TerrainData terrainData;

    private void OnEnable()
    {
        terrain = GetComponent<Terrain>();
        terrainData = Terrain.activeTerrain.terrainData;
    }

    public void Sine(float depth)
    {
        terrainData.size = new Vector3(2000f, depth, 2000f);

        int xRes = terrainData.heightmapWidth;
        int yRes = terrainData.heightmapHeight;

        float[,] heights = terrainData.GetHeights(0, 0, xRes, yRes);

        for (int j = 0; j < yRes; j++)
        {
            for (int i = 0; i < xRes; i++)
            {
                float x = (float)i / xRes * 20f;
                float y = (float)j / yRes * 20f;

                heights[i, j] = Mathf.Sin(x * y);
            }
        }
        terrainData.SetHeights(0, 0, heights);
    }


    public void RandomTerrain(float x, float y)
    {
        terrainData.size = new Vector3(2000f, 600f, 2000f);

        int xRes = terrainData.heightmapWidth;
        int yRes = terrainData.heightmapHeight;

        float[,] heights = terrainData.GetHeights(0, 0, xRes, yRes);

        for (int j = 0; j < yRes; j++)
        {
            for (int i = 0; i < xRes; i++)
            {
                float randFloat = Random.Range(x, y);

                heights[i, j] = randFloat;
            }
        }
        terrainData.SetHeights(0, 0, heights);
    }

    public void PerlinNoise()
    {
        terrainData.size = new Vector3(2000f, 200f, 2000f);

        int xRes = terrainData.heightmapWidth;
        int yRes = terrainData.heightmapHeight;

        float scale = Random.Range(10f, 30f);

        float[,] heights = terrainData.GetHeights(0, 0, xRes, yRes);

        for (int j = 0; j < yRes; j++)
        {
            for (int i = 0; i < xRes; i++)
            {
                float xCoord = (float)i / xRes * scale;
                float yCoord = (float)j / yRes * scale;
 
                heights[i, j] = Mathf.PerlinNoise(xCoord, yCoord);
            }
        }

        terrainData.SetHeights(0, 0, heights);
    }

    public class PerlinParameters {

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
        float maxValue = 0;
        for (int i = 0; i < oct; i++)
        {

            total += Mathf.PerlinNoise(x * frequency, y * frequency) * amplitude;
            maxValue += amplitude;
            amplitude *= persistance;
            frequency *= 2;
        }

        return total / maxValue;
    }

    public void MultiplePerlin()
    {
        terrainData.size = new Vector3(2000f, 600f, 2000f);

        float mPerlinHeightScale = 0.09f;

        int xRes = terrainData.heightmapWidth;
        int yRes = terrainData.heightmapHeight;

        float[,] heights = terrainData.GetHeights(0, 0, xRes, yRes);

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

    //public void LoadTexture()
    //{
    //    //int xRes = terrainData.heightmapWidth;
    //    //int yRes = terrainData.heightmapHeight;
    //    //float[,] heights = terrainData.GetHeights(0, 0, xRes, yRes);
    //    float[,] heightMap;
    //    heightMap = new float[terrainData.heightmapWidth,
    //                terrainData.heightmapHeight];

    //    for (int x = 0; x < terrainData.heightmapWidth; x++)
    //    {
    //        for (int z = 0; z < terrainData.heightmapHeight; z++)
    //        {
    //            heightMap[x, z] = heightMapImage.GetPixel(
    //                (int)(x * heightMapScale.x),
    //                (int)(z * heightMapScale.z)).grayscale * heightMapScale.y;
    //        }
    //    }
    //    terrainData.SetHeights(0, 0, heightMap);
    //}

    //public void Voronoi(int x, int y) {

    //    int xRes = terrainData.heightmapWidth;
    //    int yRes = terrainData.heightmapHeight;

    //    float[,] heights = terrainData.GetHeights(0, 0, xRes, yRes);

    //    float randHeight = Random.Range(0.0f, 1.0f);

    //    float point = heights[x, y];
    //    point = randHeight;
    //    Debug.Log("Random Point" + point);
    //    Debug.Log("RandomHeight val" + randHeight);

    //    float slope = (randHeight / 0.1f) * 1000;
    //    Debug.Log("Calc slope" + slope);

    //    //for (i = 0, ; i < slope; i++)
    //    //{
    //    //    for (j = 0; j < slope; j++)
    //    //    {

    //    //    }
    //    //}

    //    terrainData.SetHeights(x, y, heights);
    //}

    public void MidpointDisplacement()
    {
        terrainData.size = new Vector3(2000f, 600f, 2000f);

        int xRes = terrainData.heightmapWidth;
        int yRes = terrainData.heightmapHeight;

        float[,] heights = terrainData.GetHeights(0, 0, xRes, yRes);

        int width = xRes - 1;
        int height = yRes - 1;
        int squareSize = width;
        int cornerX, cornerY;
        int midX, midY;
        int pmidXL, pmidXR, pmidYU, pmidYD;

        float MPDheightMin = -2.0f;
        float MPDheightMax = 2.0f;
        float heightMin = MPDheightMin;
        float heightMax = MPDheightMax;
        float MPDheightDampenerPower = 2.0f;
        float MPDroughness = 2.0f;
        float heightDampener = Mathf.Pow(MPDheightDampenerPower, -1 * MPDroughness);

        heights[0, 0] = Random.Range(0f, 0.2f);
        heights[0, height] = Random.Range(0f, 0.2f);
        heights[width, 0] = Random.Range(0f, 0.2f);
        heights[width, height] = Random.Range(0f, 0.2f);

        while (squareSize > 0)
        {
            for (int j = 0; j < width; j += squareSize)
            {
                for (int i = 0; i < width; i += squareSize)
                {
                    cornerX = i + squareSize;
                    cornerY = j + squareSize;
                    midX = (int)(i + squareSize / 2.0f);
                    midY = (int)(j + squareSize / 2.0f);
                    heights[midX, midY] = (heights[i, j] + heights[cornerX, j] + heights[i, cornerY]
                                        + heights[cornerX, cornerY]) / 4.0f + Random.Range(heightMin, heightMax);

                }
            }

            for (int j = 0; j < width; j += squareSize)
            {
                for (int i = 0; i < width; i += squareSize)
                {
                    cornerX = i + squareSize;
                    cornerY = j + squareSize;
                    midX = (int)(i + squareSize / 2.0f);
                    midY = (int)(j + squareSize / 2.0f);
                    pmidXR = midX + squareSize;
                    pmidYU = midY + squareSize;
                    pmidXL = midX - squareSize;
                    pmidYD = midY - squareSize;
                
                    if (pmidXL <= 0 || pmidYD <= 0
                        || pmidXR >= width - 1 || pmidYU >= width - 1) continue;

                    heights[midX, j] = (heights[midX, midY] +
                                                  heights[i, j] +
                                                  heights[midX, pmidYD] +
                                                  heights[cornerX, j]) / 4.0f +
                                                  Random.Range(heightMin, heightMax);

                    heights[midX, cornerY] = (heights[i, cornerY] +
                                                            heights[midX, midY] +
                                                            heights[cornerX, cornerY] +
                                                        heights[midX, pmidYU]) / 4.0f +
                                                        Random.Range(heightMin, heightMax);

                    heights[i, midY] = (heights[i, j] +
                                                            heights[pmidXL, midY] +
                                                            heights[i, cornerY] +
                                                  heights[midX, midY]) / 4.0f +
                                                  Random.Range(heightMin, heightMax);

                    heights[cornerX, midY] = (heights[midX, j] +
                                                            heights[midX, midY] +
                                                            heights[cornerX, cornerY] +
                                                            heights[pmidXR, midY]) / 4.0f +
                                                        Random.Range(heightMin, heightMax);
                }
            }

            squareSize = (int)(squareSize / 2.0f);

            heightMin *= heightDampener;
            heightMax *= heightDampener;
        }
        terrainData.SetHeights(0, 0, heights);
    }

    public void FlatTerrain()
    {
        terrainData.size = new Vector3(2000f, 600f, 2000f);

        int xRes = terrainData.heightmapWidth;
        int yRes = terrainData.heightmapHeight;

        float[,] heightMap = new float[xRes, yRes];

        for (int z = 0; z < yRes; z++)
        {
            for (int x = 0; x < xRes; x++)
            {
                heightMap[x, z] = 0f;
            }
        }

        terrainData.SetHeights(0, 0, heightMap);
    }
}
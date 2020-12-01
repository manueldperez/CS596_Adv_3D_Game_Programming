using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PGCTerrain))]
[CanEditMultipleObjects]

public class PGCTerrainEditor : Editor
{
    float x;
    float y;

    float depth;

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        PGCTerrain terrain = (PGCTerrain)target;
        EditorGUIUtility.fieldWidth = 5;


        //Random Mesh function
        GUILayout.Label("Random Mesh", EditorStyles.boldLabel);
        GUILayout.Label("Set Heights Between Random Values from 0 to 1");

        x = EditorGUILayout.FloatField("Min Range", x);
        y = EditorGUILayout.FloatField("Max Range", y);

        if (GUILayout.Button("Random Heights"))
        {
            terrain.RandomTerrain(x, y);
        }


        //Sine function
        GUILayout.Space(15);
        GUILayout.Label("Sine", EditorStyles.boldLabel);
        GUILayout.Label("Min Depth recommended: 80");

        depth = EditorGUILayout.FloatField("Depth", depth);

        if (GUILayout.Button("Sine"))
        {
            terrain.Sine(depth);
        }


        //Perlin Noise function
        GUILayout.Space(15);
        GUILayout.Label("Single Perlin Noise", EditorStyles.boldLabel);

        if (GUILayout.Button("Single Perlin Noise"))
        {
            terrain.PerlinNoise();
        }


        //Multiple Perlin Noise function
        GUILayout.Space(15);
        GUILayout.Label("Multiple Perlin Noise", EditorStyles.boldLabel);

        if (GUILayout.Button("Apply Multiple Perlin"))
        {
            terrain.MultiplePerlin();
        }


        //    GUILayout.Space(15);
        //    GUILayout.Label("Voronoi", EditorStyles.boldLabel);

        //    EditorGUILayout.BeginHorizontal();
        //    x = EditorGUILayout.FloatField("Random Point", x);
        //    y = EditorGUILayout.FloatField("Y", y);
        //    EditorGUILayout.EndHorizontal();

        //    if (GUILayout.Button("Voronoi"))
        //    {
        //        terrain.Voronoi(xVal, yVal);
        //    }


        //Midpoint Displacement function
        GUILayout.Space(15);
        GUILayout.Label("Midpoint Displacement", EditorStyles.boldLabel);

        if (GUILayout.Button("MDP"))
        {
            terrain.MidpointDisplacement();
        }


        //    GUILayout.Space(15);
        //    GUILayout.Label("Load Heights From Texture", EditorStyles.boldLabel);
        //    EditorGUILayout.PropertyField(heightMapImage);
        //    EditorGUILayout.PropertyField(heightMapScale);
        //if (GUILayout.Button("Load Texture"))
        //{
        //    terrain.LoadTexture();
        //}


        //Flatten Terrain function
        GUILayout.Space(15);
        GUILayout.Label("Flatten Terrain", EditorStyles.boldLabel);
        if (GUILayout.Button("Reset Terrain"))
        {
            terrain.FlatTerrain();
        }
    }
}
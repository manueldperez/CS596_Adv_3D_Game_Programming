using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Perlin_Terrain))]
[CanEditMultipleObjects]

public class Perlin_TerrainEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        Perlin_Terrain terrain = (Perlin_Terrain)target;
        EditorGUIUtility.fieldWidth = 5;


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
    }
}

using System;
using UnityEditor.Build;
using UnityEngine;

public class TunnelSection : MonoBehaviour
{
    
    private MeshFilter[] meshFilters;
    private TunnelFace[] tunnelFaces;
    private int zNoiseOffset;
    private Vector3[] orientations = { Vector3.up, Vector3.right, Vector3.down, Vector3.left };

    public void CreateSection(ShapeSettings settings, int zOffset)
    {
        this.zNoiseOffset = zOffset;

        if (meshFilters == null || meshFilters.Length == 0)
        {
            meshFilters = new MeshFilter[4];
        }
        tunnelFaces = new TunnelFace[4];

        for (int i = 0; i < 4; i++)
        {
            if (meshFilters[i] == null)
            {
                GameObject meshObj = new GameObject("mesh");
                meshObj.transform.parent = transform;
                meshObj.AddComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Standard"));
                meshObj.AddComponent<MeshFilter>();
                meshFilters[i] = meshObj.GetComponent<MeshFilter>();
                meshFilters[i].sharedMesh = new Mesh();
            }

            tunnelFaces[i] = new TunnelFace(settings, meshFilters[i].sharedMesh, this.orientations[i], this.zNoiseOffset);
        }

        foreach (TunnelFace face in tunnelFaces)
        {
            face.ConstructMesh();
        }
    }

    public void UpdateSection(ShapeSettings settings)
    {
        for (int i = 0; i < 4; i++)
        {
            tunnelFaces[i].settings = settings;
            tunnelFaces[i].ConstructMesh();
        }
    }

}

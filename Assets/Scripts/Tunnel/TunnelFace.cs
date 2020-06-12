using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelFace
{
    public ShapeSettings settings;


    private Noise noise = new Noise();
    private Mesh mesh;
    private Vector3 localUp;
    private Vector3 axisA;
    private Vector3 axisB;
    private int zNoiseOffset;


    public TunnelFace(ShapeSettings settings, Mesh mesh, Vector3 localUp, int zNoiseOffset)
    {
        this.mesh = mesh;
        this.localUp = localUp;
        axisA = new Vector3(localUp.y, localUp.z, localUp.x);
        axisB = Vector3.Cross(localUp, axisA);
        this.settings = settings;
        this.zNoiseOffset = zNoiseOffset;
    }


    public float evaluate(Vector3 point)
    {
        float noiseValue = settings.radius + (noise.Evaluate(point * settings.noiseRoughness + new Vector3(0, 0, this.zNoiseOffset * settings.noiseRoughness)) + 1) * .5f;
        return noiseValue * settings.noiseStrength;
    }

    public void ConstructMesh()
    {
        Vector3[] verticies = new Vector3[settings.resolution * settings.resolution];
        int[] triangles = new int[(settings.resolution - 1) * (settings.resolution - 1) * 2 * 3];
        int triangleIndex = 0;

        for (int x = 0; x < settings.resolution; x++)
        {
            for (int y = 0; y < settings.resolution; y++)
            {
                int i = x + y * settings.resolution;
                Vector2 percent = new Vector2(x, y) / (settings.resolution - 1);
                Vector3 pointOnUnitCube = localUp * -1 + (percent.x - .5f) * 2 * axisA + (percent.y - .5f) * 2 * axisB;
                Vector3 pointOnUnitCylinder = new Vector2(pointOnUnitCube.x, pointOnUnitCube.y).normalized;

                pointOnUnitCylinder *= evaluate(new Vector3(pointOnUnitCylinder.x, pointOnUnitCylinder.y, pointOnUnitCube.z));


                verticies[i] = new Vector3(pointOnUnitCylinder.x, pointOnUnitCylinder.y, pointOnUnitCube.z);

                if (x != settings.resolution -1 && y != settings.resolution - 1)
                {
                    triangles[triangleIndex] = i;
                    triangles[triangleIndex + 1] = i + settings.resolution + 1;
                    triangles[triangleIndex + 2] = i + settings.resolution;

                    triangles[triangleIndex + 3] = i;
                    triangles[triangleIndex + 4] = i + 1;
                    triangles[triangleIndex + 5] = i + settings.resolution + 1;

                    triangleIndex += 6;
                }
            }
        }

        this.mesh.Clear();
        this.mesh.vertices = verticies;
        this.mesh.triangles = triangles;
        this.mesh.RecalculateNormals();
    }

}

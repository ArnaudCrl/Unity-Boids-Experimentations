using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PointsOnSphere
{
    const int numberOfRays = 150;
    public static readonly Vector3[] directions;


    static PointsOnSphere()
    {
        directions = new Vector3[PointsOnSphere.numberOfRays];
        float angleIncrement = Mathf.PI * 2 * (1 + Mathf.Sqrt(5)) / 2;
        for (int i = 0; i < numberOfRays; ++i)
        {
            float t = (float)i / numberOfRays;
            float inclination = Mathf.Acos(1 - 2 * t);
            float azimuth = angleIncrement * i;

            float x = Mathf.Sin(inclination) * Mathf.Cos(azimuth);
            float y = Mathf.Sin(inclination) * Mathf.Sin(azimuth);
            float z = Mathf.Cos(inclination);
            directions[i] = new Vector3(x, y, z);
        }
    }
}

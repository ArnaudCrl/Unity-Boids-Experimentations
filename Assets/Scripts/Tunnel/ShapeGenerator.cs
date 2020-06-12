using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeGenerator
{
    Noise noise = new Noise();
    ShapeSettings settings;
    private int zOffset;

    public ShapeGenerator(ShapeSettings settings, int zOffset)
    {
        this.settings = settings;
        this.zOffset = zOffset;
    }

    public float evaluate(Vector3 point)
    {
        float noiseValue = settings.radius + (noise.Evaluate(point * settings.noiseRoughness + new Vector3(0, 0, zOffset * settings.noiseRoughness) )+ 1) * .5f;
        return noiseValue * settings.noiseStrength;
    }
}

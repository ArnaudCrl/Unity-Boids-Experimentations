using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ShapeSettings : ScriptableObject   
{
    public int resolution = 10;
    public float radius = 1;
    public float noiseStrength = 1;
    public float noiseRoughness = 1;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BoidSettings : ScriptableObject
{
    public float minSpeed = 10.0f;
    public float maxSpeed = 15.0f;
    public float maxForce = 0.2f;

    public float CohesionRadius = 15.0f;
    public float SeparationRadius = 3.0f;
    [Range(0, 180)] public float viewAngle = 90.0f;
}

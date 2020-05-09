using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BoidSettings : ScriptableObject
{
    public float maxSpeed = 15.0f;

    public float CohesionRadius = 6.0f;
    public float SeparationRadius = 3.0f;
    [Range(0, 180)] public float viewAngle = 90.0f;

    public float cohesionMaxForce = 0.3f;
    public float alignmentMaxForce = 0.3f;
    public float separationMaxForce = 1.0f;
    public float obstacleAvoidancenMaxForce = 2.0f;
    public float huntMaxForce = 1.0f;
}

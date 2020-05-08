using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BoidSettings : ScriptableObject
{
    public float maxSpeed;
    public float maxForce;

    public float CohesionRadius;
    public float SeparationRadius;
    [Range(0, 180)] public float viewAngle;
}

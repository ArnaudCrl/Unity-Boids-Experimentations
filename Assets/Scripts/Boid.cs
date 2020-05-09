using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class Boid : MonoBehaviour
{
    public BoidSettings settings;

    [HideInInspector] public Vector3 position;
    [HideInInspector] public Vector3 velocity;

    public Vector3 flockCenter;
    public Vector3 flockMovingDirection;
    public Vector3 flockSeparationDirection;
    public int numberOfNeighbours = 0;

    private Vector3 cohesionSteer;
    private Vector3 alignmentSteer;
    private Vector3 separationSteer;

    private Vector3 acceleration;


    void Start()
    {
        this.position = transform.position;
        this.velocity = transform.forward.normalized * settings.maxSpeed;
    }

    void Update()
    {
        ComputeAcceleration();

        this.velocity += this.acceleration;
        if (this.velocity.magnitude < settings.maxSpeed * 0.7)
        {
            this.velocity = this.velocity.normalized;
            this.velocity *= (settings.maxSpeed * 0.7f);
        }
        this.transform.position += Vector3.ClampMagnitude(this.velocity, settings.maxSpeed) * Time.deltaTime;
        this.position = this.transform.position;
        this.transform.rotation = Quaternion.LookRotation(velocity);

        ResetValues();
    }

    private void ComputeAcceleration()
    {
        if (numberOfNeighbours > 0)
        {
            //Debug.DrawRay(transform.position, flockSeparationDirection, Color.red);
            cohesionSteer = SteerTowards((flockCenter / numberOfNeighbours) - this.transform.position, settings.cohesionMaxForce);
            alignmentSteer = SteerTowards(flockMovingDirection, settings.alignmentMaxForce);
            separationSteer = SteerTowards(flockSeparationDirection, settings.separationMaxForce);
            //Debug.DrawRay(transform.position, separationSteer, Color.yellow);
            acceleration += cohesionSteer;
            acceleration += alignmentSteer;
            acceleration += separationSteer;
        }
    }

    private Vector3 SteerTowards(Vector3 direction, float maxForce)
    {
        direction = direction.normalized * settings.maxSpeed;
        direction -= velocity;
        direction = Vector3.ClampMagnitude(direction, maxForce);
        return direction;
    }

    private void ResetValues()
    {
        this.flockCenter = Vector3.zero;
        this.flockMovingDirection = Vector3.zero;
        this.flockSeparationDirection = Vector3.zero;
        this.acceleration = Vector3.zero;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class Boid : MonoBehaviour
{
    public BoidSettings settings;

    [HideInInspector] public Vector3 position;
    [HideInInspector] public Vector3 velocity;

    [HideInInspector] public Vector3 flockCenter = Vector3.zero;
    [HideInInspector] public Vector3 flockMovingDirection = Vector3.zero;
    [HideInInspector] public Vector3 flockSeparationDirection = Vector3.zero;
    [HideInInspector] public int numberOfNeighbours = 0;

    private Vector3 cohesionSteer;
    private Vector3 alignmentSteer;
    private Vector3 separationSteer;

    private Vector3 acceleration;

    private void ComputeAcceleration()
    {
        if (numberOfNeighbours > 0)
        {
            acceleration = Vector3.zero;

            cohesionSteer = SteerTowards((flockCenter / numberOfNeighbours) - this.position);
            alignmentSteer = SteerTowards(flockMovingDirection);
            separationSteer = SteerTowards(flockSeparationDirection);

            acceleration += cohesionSteer;
            acceleration += alignmentSteer;
            acceleration += separationSteer;
        }
    }

    private Vector3 SteerTowards(Vector3 direction)
    {
        direction = direction.normalized * settings.maxSpeed;
        direction -= velocity;
        direction = Vector3.ClampMagnitude(direction, settings.maxForce);
        return direction;
    }

    void Start()
    {
        this.position = transform.position;
        this.velocity = transform.forward.normalized * settings.maxSpeed;
    }

    private float speed;

    void Update()
    {
        ComputeAcceleration();

        velocity += acceleration * Time.deltaTime;
        speed = Mathf.Clamp(velocity.magnitude, settings.minSpeed, settings.maxSpeed);
        velocity = velocity.normalized * speed;

        this.transform.position += velocity * Time.deltaTime;
        this.position = this.transform.position;
        this.transform.rotation = Quaternion.LookRotation(velocity);
    }
}

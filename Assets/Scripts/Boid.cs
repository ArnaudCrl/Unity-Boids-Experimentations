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

    [HideInInspector] public Vector3 flockCenter;
    [HideInInspector] public Vector3 flockMovingDirection;
    [HideInInspector] public Vector3 flockSeparationDirection;
    [HideInInspector] public int numberOfNeighbours = 0;


    public List<Collider> GetNeighbourBoids(float radius, float viewAngle)
    {
        List<Collider> boids = Physics.OverlapSphere(transform.position, radius).ToList();
        Collider selfCollider = GetComponent<Collider>();

        if (boids.Contains(selfCollider))
        {
            boids.Remove(selfCollider);
        }
        boids.RemoveAll(collider => Vector3.Angle(transform.forward, collider.gameObject.transform.position - this.transform.position) > viewAngle);
        boids.RemoveAll(collider => collider.gameObject.layer != this.gameObject.layer);
        return boids;
    }

    private void UpdateVelocity()
    {
        if (numberOfNeighbours > 0)
        {
            Vector3 cohesionSteer = SteerTowards((flockCenter / numberOfNeighbours) - this.position);
            Vector3 alignmentSteer = SteerTowards(flockMovingDirection);
            Vector3 separationSteer = SteerTowards(flockSeparationDirection);

            this.velocity += (cohesionSteer + alignmentSteer + separationSteer); // * Time.deltaTime;
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



    void Update()
    {
        this.position = transform.position;

        UpdateVelocity();
        this.transform.position += Vector3.ClampMagnitude(this.velocity, settings.maxSpeed) * Time.deltaTime;
        this.transform.rotation = Quaternion.LookRotation(velocity);
    }
}

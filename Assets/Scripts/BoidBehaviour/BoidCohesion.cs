using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidCohesion : MonoBehaviour
{
    public float radius;
    public float viewAngle;

    private Boid boid;
    private Vector3 cohesionSteer;
    private List<Collider> neighbours;


    private Vector3 Cohesion(List<Collider> nearbyBoids)
    {
        Vector3 steer = Vector3.zero;
        Vector3 vectorSum = Vector3.zero;

        foreach (Collider boid in nearbyBoids)
        {
            vectorSum += boid.gameObject.transform.position;
        }
        if (vectorSum != Vector3.zero)
        {
            steer = (vectorSum / nearbyBoids.Count).normalized;
            steer *= boid.settings.maxSpeed;
            steer -= transform.position;
            steer = Vector3.ClampMagnitude(steer, boid.settings.maxForce);
        }
        return steer;
    }

    void Start()
    {
        boid = GetComponent<Boid>();
    }

    void Update()
    {
        neighbours = boid.GetNeighbourBoids(radius, viewAngle);
        cohesionSteer = Cohesion(neighbours);
        boid.velocity += cohesionSteer;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(this.transform.position, radius);

        Gizmos.color = Color.red;
        foreach (Collider boid in neighbours)
        {
            Gizmos.DrawRay(this.transform.position, boid.gameObject.transform.position - transform.position);
        }

        Gizmos.color = Color.green;
        Gizmos.DrawRay(this.transform.position, cohesionSteer.normalized * boid.settings.maxSpeed);
    }
}

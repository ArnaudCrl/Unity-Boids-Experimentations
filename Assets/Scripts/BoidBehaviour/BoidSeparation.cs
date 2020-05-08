using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidSeparation : MonoBehaviour
{
    public float radius;
    public float viewAngle;

    private Boid boid;
    private List<Collider> neighbours;
    private Vector3 separationSteer;

    private Vector3 Separation(List<Collider> nearbyBoids)
    {
        Vector3 steer = Vector3.zero;
        Vector3 vectorSum = Vector3.zero;

        foreach (Collider boid in nearbyBoids)
        {
            Vector3 neighbour = (transform.position - boid.gameObject.transform.position).normalized;
            neighbour /= Vector3.Distance(transform.position, boid.gameObject.transform.position);

            vectorSum += neighbour;
        }
        if (vectorSum != Vector3.zero)
        {
            steer = (vectorSum / nearbyBoids.Count).normalized;
            steer *= boid.settings.maxSpeed;
            steer -= boid.velocity;
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
        separationSteer = Separation(neighbours);
        boid.velocity += separationSteer;
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
        Gizmos.DrawRay(this.transform.position, separationSteer.normalized * boid.settings.maxSpeed);
    }
}

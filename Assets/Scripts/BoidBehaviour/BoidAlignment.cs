using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Boid))]
public class BoidAlignment : MonoBehaviour
{
    public float radius;
    public float viewAngle;

    private Boid boid;
    private List<Collider> neighbours;
    private Vector3 alignmentSteer;

    private Vector3 Alignment(List<Collider> nearbyBoids)
    {
        Vector3 steer = Vector3.zero;
        Vector3 vectorSum = Vector3.zero;

        foreach (Collider b in nearbyBoids)
        {
            vectorSum += b.gameObject.GetComponent<Boid>().velocity;
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
        alignmentSteer = Alignment(neighbours);
        boid.velocity += alignmentSteer;
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
        Gizmos.DrawRay(this.transform.position, alignmentSteer.normalized * boid.settings.maxSpeed);
    }
}

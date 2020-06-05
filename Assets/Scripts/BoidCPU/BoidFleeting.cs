using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoidFleeting : MonoBehaviour
{
    private Boid boid;
    private List<Collider> neighbours;
    private Vector3 separationSteer;


    void Start()
    {
        boid = GetComponent<Boid>();
    }

    private List<Collider> GetNeighbourBoids(float radius, float viewAngle)
    {
        neighbours = new List<Collider>();
        neighbours = Physics.OverlapSphere(transform.position, radius).ToList();
        Collider selfCollider = GetComponent<Collider>();

        if (neighbours.Contains(selfCollider))
        {
            neighbours.Remove(selfCollider);
        }
        neighbours.RemoveAll(collider => Vector3.Angle(transform.forward, collider.gameObject.transform.position - this.transform.position) > viewAngle);
        neighbours.RemoveAll(collider => collider.gameObject.layer != this.gameObject.layer);
        return neighbours;
    }

    void Update()
    {
        neighbours = GetNeighbourBoids(boid.settings.SeparationRadius, boid.settings.viewAngle);
        separationSteer = Separation(neighbours);
        boid.velocity += separationSteer;
    }

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
            steer = Vector3.ClampMagnitude(steer, boid.settings.separationMaxForce);
        }
        return steer;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(this.transform.position, boid.settings.SeparationRadius);

        Gizmos.color = Color.red;
        foreach (Collider boid in neighbours)
        {
            Gizmos.DrawRay(this.transform.position, boid.gameObject.transform.position - transform.position);
        }

        Gizmos.color = Color.green;
        Gizmos.DrawRay(this.transform.position, separationSteer.normalized * boid.settings.maxSpeed);
    }
}

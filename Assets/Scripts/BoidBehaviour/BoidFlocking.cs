using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoidFlocking : MonoBehaviour
{
    private Boid self;
    private List<Collider> neighbours = new List<Collider>();

    void Start()
    {
        self = GetComponent<Boid>();
    }

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

    void Update()
    {
        self.numberOfNeighbours = 0;

        neighbours = GetNeighbourBoids(self.settings.CohesionRadius, self.settings.viewAngle);
        foreach (Collider b in neighbours)
        {
            self.flockMovingDirection += b.gameObject.GetComponent<Boid>().velocity;
            self.flockCenter += b.gameObject.GetComponent<Boid>().position;
            self.numberOfNeighbours++;
        }

        neighbours = GetNeighbourBoids(self.settings.SeparationRadius, self.settings.viewAngle);
        foreach (Collider b in neighbours)
        {
            self.flockSeparationDirection -= (b.gameObject.GetComponent<Boid>().position - self.position) / Vector3.Distance(b.gameObject.GetComponent<Boid>().position, self.position);
        }
    }
}

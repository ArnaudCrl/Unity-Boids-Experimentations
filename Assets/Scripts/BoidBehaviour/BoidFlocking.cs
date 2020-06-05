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

    private void GetNeighbourBoids(float radius, float viewAngle)
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
    }

    void Update()
    {
        ResetValues();

        GetNeighbourBoids(self.settings.CohesionRadius, self.settings.viewAngle);
        foreach (Collider b in neighbours)
        {
            self.flockMovingDirection += b.gameObject.transform.forward;
            self.flockCenter += b.gameObject.transform.position;
            self.numberOfNeighbours++;
        }

        GetNeighbourBoids(self.settings.SeparationRadius, self.settings.viewAngle);
        foreach (Collider b in neighbours)
        {
            self.flockSeparationDirection -= (b.gameObject.transform.position - this.transform.position)
                / (Vector3.Distance(b.gameObject.transform.position, this.transform.position) * Vector3.Distance(b.gameObject.transform.position, this.transform.position));
        }
    }

    private void ResetValues()
    {
        self.numberOfNeighbours = 0;
        self.flockCenter = Vector3.zero;
        self.flockMovingDirection = Vector3.zero;
        self.flockSeparationDirection = Vector3.zero;
    }
}

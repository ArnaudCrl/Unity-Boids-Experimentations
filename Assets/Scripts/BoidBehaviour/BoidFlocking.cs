using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidFlocking : MonoBehaviour
{
    private Boid self;
    private Boid[] neighbours;

    void Start()
    {
        self = GetComponent<Boid>();
        neighbours = FindObjectsOfType<Boid>();
    }

    void Update()
    {
        foreach(Boid b in neighbours)
        {
            if (b != self)
            {
                float distance = Vector3.Distance(b.position, self.position);
                if (distance < self.settings.CohesionRadius)
                {
                    self.flockMovingDirection += b.velocity;
                    self.flockCenter += b.position;
                    self.numberOfNeighbours++;
                }
                if (distance < self.settings.SeparationRadius)
                {
                    self.flockSeparationDirection -= (b.position - self.position) / distance;
                    
                }
            }
        }
    }
}

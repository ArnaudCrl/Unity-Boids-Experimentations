using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[RequireComponent(typeof(Boid))]
public class BoidObstacleAvoidance : MonoBehaviour
{
    public float radius;

    private Vector3[] rays;
    private Boid boid;
    LayerMask obstacleMask;

    void Start()
    {
        obstacleMask = LayerMask.GetMask("Obstacle");
        boid = GetComponent<Boid>();
    }

    void Update()
    {
        Vector3 steer = Vector3.zero;

        rays = PointsOnSphere.directions;
        int index = 0;
        while (Physics.Raycast(this.transform.position, this.transform.TransformDirection(rays[index]), radius, obstacleMask))
        {
            index++;
        }

        if (index != 0)
        {
            steer = this.transform.TransformDirection(rays[index]).normalized;
            steer *= boid.settings.maxSpeed;
            steer -= boid.velocity;
            steer = Vector3.ClampMagnitude(steer, boid.settings.maxForce * 20);
        }


        boid.velocity += steer;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        int index = 0;
        while (Physics.Raycast(this.transform.position, this.transform.TransformDirection(rays[index]), radius, obstacleMask))
        {
            Gizmos.DrawRay(this.transform.position, this.transform.TransformDirection(rays[index]).normalized * radius);
            index++;
        }
        Gizmos.color = Color.green;
        Gizmos.DrawRay(this.transform.position, this.transform.TransformDirection(rays[index]).normalized * radius);
    }
}

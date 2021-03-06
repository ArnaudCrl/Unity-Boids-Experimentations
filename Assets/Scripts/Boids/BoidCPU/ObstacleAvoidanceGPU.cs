﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class ObstacleAvoidanceGPU : MonoBehaviour
{
    private BoidGPU boid;
    private LayerMask obstacleMask;

    void Start()
    {
        obstacleMask = LayerMask.GetMask("Obstacle");
        boid = GetComponent<BoidGPU>();
    }

    void Update()
    {
        Vector3 steer = Vector3.zero;

        int index = 0;
        while (Physics.Raycast(this.transform.position, this.transform.TransformDirection(PointsOnSphere.directions[index]), boid.settings.ObstacleAvoidanceRadius, obstacleMask))
        {
            index++;
            if (index == PointsOnSphere.directions.Length - 1)
            {
                break;
            }
        }

        if (index != 0)
        {
            steer = this.transform.TransformDirection(PointsOnSphere.directions[index]).normalized;
            steer *= boid.settings.maxSpeed;
            steer -= boid.velocity;
            steer = Vector3.ClampMagnitude(steer, boid.settings.obstacleAvoidancenMaxForce);
        }



        boid.velocity += steer;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        int index = 0;
        while (Physics.Raycast(this.transform.position, this.transform.TransformDirection(PointsOnSphere.directions[index]), boid.settings.ObstacleAvoidanceRadius, obstacleMask))
        {
            Gizmos.DrawRay(this.transform.position, this.transform.TransformDirection(PointsOnSphere.directions[index]).normalized * boid.settings.ObstacleAvoidanceRadius);
            index++;
        }
        Gizmos.color = Color.green;
        Gizmos.DrawRay(this.transform.position, this.transform.TransformDirection(PointsOnSphere.directions[index]).normalized * boid.settings.ObstacleAvoidanceRadius);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(BoidGPU))]
public class BoidHunt : MonoBehaviour
{
    public GameObject target;

    private Vector3 targetPos;
    private BoidGPU boid;

    void Start()
    {
        target = GameObject.FindWithTag("Prey");
        boid = GetComponent<BoidGPU>();
    }

    void Update()
    {
        targetPos = target.transform.position;
        Vector3 steer = this.targetPos - this.transform.position;
        steer *= boid.settings.maxSpeed;
        steer -= boid.velocity;
        steer = Vector3.ClampMagnitude(steer, boid.settings.huntMaxForce);

        boid.velocity += steer;

    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(this.transform.position, targetPos - this.transform.position);
    }
}

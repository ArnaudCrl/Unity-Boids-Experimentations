using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Boid))]
public class BoidHunt : MonoBehaviour
{
    public GameObject target;

    private Vector3 targetPos;
    private Boid boid;

    void Start()
    {
        boid = GetComponent<Boid>();
        GameObject temp = GameObject.FindGameObjectWithTag("Prey");
        target = temp;
    }

    void Update()
    {
        targetPos = target.gameObject.transform.position;
        Vector3 steer = this.targetPos - this.transform.position;
        steer *= boid.settings.maxSpeed;
        steer -= boid.velocity;
        steer = Vector3.ClampMagnitude(steer, boid.settings.maxForce);

        boid.velocity += steer;

    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(this.transform.position, targetPos - this.transform.position);
    }
}

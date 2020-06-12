using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidManager : MonoBehaviour
{
    public BoidSettings settings;
    public ComputeShader compute;
    public BoidGPU[] boids;
    const int threadGroupSize = 1024;
    public float fps;
    void Start()
    {
        boids = FindObjectsOfType<BoidGPU>();
    }

    void Update()
    {
        fps = 1f / Time.deltaTime;

        if (boids.Length > 0)
        {

            int numBoids = boids.Length;
            var boidData = new BoidData[numBoids];

            for (int i = 0; i < boids.Length; i++)
            {
                boidData[i].position = boids[i].position;
                boidData[i].direction = boids[i].transform.forward.normalized;
            }

            var boidBuffer = new ComputeBuffer(numBoids, BoidData.Size);
            boidBuffer.SetData(boidData);

            compute.SetBuffer(0, "boids", boidBuffer);
            compute.SetInt("numBoids", boids.Length);
            compute.SetFloat("viewRadius", settings.CohesionRadius);
            compute.SetFloat("avoidRadius", settings.SeparationRadius);

            int threadGroups = Mathf.CeilToInt(numBoids / (float)threadGroupSize);
            compute.Dispatch(0, threadGroups, 1, 1);

            boidBuffer.GetData(boidData);

            for (int i = 0; i < boids.Length; i++)
            {
                boids[i].flockMovingDirection = boidData[i].flockHeading;
                boids[i].flockCenter = boidData[i].flockCentre;
                boids[i].flockSeparationDirection = boidData[i].avoidanceHeading;
                boids[i].numberOfNeighbours = boidData[i].numFlockmates;

                boids[i].ComputeAcceleration();
            }

            boidBuffer.Release();
        }
    }

    public struct BoidData
    {
        public Vector3 position;
        public Vector3 direction;

        public Vector3 flockHeading;
        public Vector3 flockCentre;
        public Vector3 avoidanceHeading;
        public int numFlockmates;

        public static int Size
        {
            get
            {
                return sizeof(float) * 3 * 5 + sizeof(int);
            }
        }
    }
}

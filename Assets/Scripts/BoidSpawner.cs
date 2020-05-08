using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidSpawner : MonoBehaviour
{
    public GameObject boidPrefab;
    public GameObject boidPrey;
    public GameObject boidsContainer;
    public int number;
    private Transform boundingBox;

    void Start()
    {
        boundingBox = boidsContainer.transform;
        Instantiate(boidsContainer);


        Vector3 randomPos = new Vector3(Random.Range(boundingBox.position.x - boundingBox.localScale.x / 2, boundingBox.position.x + boundingBox.localScale.x / 2),
                                                  Random.Range(boundingBox.position.y - boundingBox.localScale.y / 2, boundingBox.position.y + boundingBox.localScale.y / 2),
                                                  Random.Range(boundingBox.position.z - boundingBox.localScale.z / 2, boundingBox.position.z + boundingBox.localScale.z / 2));
        Instantiate(boidPrey, randomPos, Random.rotation);

        for (int i = 0; i < number; ++i)
        {
             Vector3 position = new Vector3(Random.Range(boundingBox.position.x - boundingBox.localScale.x / 2, boundingBox.position.x + boundingBox.localScale.x / 2),
                                                  Random.Range(boundingBox.position.y - boundingBox.localScale.y / 2, boundingBox.position.y + boundingBox.localScale.y / 2),
                                                  Random.Range(boundingBox.position.z - boundingBox.localScale.z / 2, boundingBox.position.z + boundingBox.localScale.z / 2));

            Instantiate(boidPrefab, position, Random.rotation);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidSpawner : MonoBehaviour
{
    public GameObject boidPrefab;
    public GameObject target;
    public GameObject boidsContainer;
    public int number;
    private Transform boundingBox;

    void Awake()
    {
        boundingBox = boidsContainer.transform;

        Vector3 randomPos = new Vector3(Random.Range(boundingBox.position.x - boundingBox.localScale.x / 2, boundingBox.position.x + boundingBox.localScale.x / 2),
                                                  Random.Range(boundingBox.position.y - boundingBox.localScale.y / 2, boundingBox.position.y + boundingBox.localScale.y / 2),
                                                  Random.Range(boundingBox.position.z - boundingBox.localScale.z / 2, boundingBox.position.z + boundingBox.localScale.z / 2));
        Instantiate(target, randomPos, Quaternion.identity);

        for (int i = 0; i < number; ++i)
        {
             Vector3 position = new Vector3(Random.Range(boundingBox.position.x - boundingBox.localScale.x / 2, boundingBox.position.x + boundingBox.localScale.x / 2),
                                                  Random.Range(boundingBox.position.y - boundingBox.localScale.y / 2, boundingBox.position.y + boundingBox.localScale.y / 2),
                                                  Random.Range(boundingBox.position.z - boundingBox.localScale.z / 2, boundingBox.position.z + boundingBox.localScale.z / 2));

            Instantiate(boidPrefab, position, Random.rotation);
        }
    }
}

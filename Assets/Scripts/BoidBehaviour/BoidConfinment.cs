using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidConfinment : MonoBehaviour
{
    public GameObject BoxObject;
    private Transform boundingBox;

    void Start()
    {
        boundingBox = BoxObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentPosition = transform.position;

        // X
        if (currentPosition.x < boundingBox.position.x - boundingBox.localScale.x / 2)
            transform.position = new Vector3(boundingBox.position.x + boundingBox.localScale.x / 2, currentPosition.y, currentPosition.z);

        if (currentPosition.x > boundingBox.position.x + boundingBox.localScale.x / 2)
            transform.position = new Vector3(boundingBox.position.x - boundingBox.localScale.x / 2, currentPosition.y, currentPosition.z);

        // Y
        if (currentPosition.y < boundingBox.position.y - boundingBox.localScale.y / 2)
            transform.position = new Vector3(currentPosition.x, boundingBox.position.y + boundingBox.localScale.y / 2, currentPosition.z);


        if (currentPosition.y > boundingBox.position.y + boundingBox.localScale.y / 2)
            transform.position = new Vector3(currentPosition.x, boundingBox.position.y - boundingBox.localScale.y / 2, currentPosition.z);

        // Z
        if (currentPosition.z < boundingBox.position.z - boundingBox.localScale.z / 2)
            transform.position = new Vector3(currentPosition.x, currentPosition.y, boundingBox.position.z + boundingBox.localScale.z / 2);


        if (currentPosition.z > boundingBox.position.z + boundingBox.localScale.z / 2)
            transform.position = new Vector3(currentPosition.x, currentPosition.y, boundingBox.position.z - boundingBox.localScale.z / 2);
    }
}

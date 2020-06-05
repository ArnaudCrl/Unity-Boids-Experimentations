using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    void Update()
    {
        Transform targetPos = GameObject.FindGameObjectWithTag("Prey").transform;
        transform.LookAt(targetPos);
    }
}

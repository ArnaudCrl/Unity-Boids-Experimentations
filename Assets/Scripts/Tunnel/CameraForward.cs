using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraForward : MonoBehaviour
{
    public float speed = 0.05f;
    void Update()
    {
        transform.position += new Vector3(0,0,speed);
    }
}

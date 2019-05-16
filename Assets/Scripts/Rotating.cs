using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotating : MonoBehaviour
{
    public bool rotateY = false;
    public float rotateYdeg = 100.0f; 
    public string rotateYdir = "Right";
    public bool rotateX = false;
    public float rotateXdeg = 100.0f;
    public string rotateXdir = "Right";
    public float rotateZdeg = 100.0f;
    public bool rotateZ = false;
    public string rotateZdir = "Right";
    public string collisionTag = "Walls";
    Rigidbody rb;
    Vector3 velo;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        if (rotateYdir == "Left")
        {
            rotateYdeg=-rotateYdeg;
        }
        if (rotateXdir == "Left")
        {
            rotateXdeg = -rotateXdeg;
        }
        if (rotateZdir == "Left")
        {
            rotateZdeg = -rotateZdeg;
        }
        if (!rotateY)
        {
            rotateYdeg = 0.0f;
        }
        if (!rotateX)
        {
            rotateXdeg = 0.0f;
        }
        if (!rotateZ)
        {
            rotateZdeg = 0.0f;
        }
        velo.Set(rotateXdeg, rotateYdeg, rotateZdeg);
    }

    private void FixedUpdate()
    {
        Quaternion deltaRotation = Quaternion.Euler(velo*Time.deltaTime);
        rb.MoveRotation(rb.rotation * deltaRotation);
    }
}

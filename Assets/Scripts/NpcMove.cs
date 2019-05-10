using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcMove : MonoBehaviour
{
    public string collisionTag = "Walls";
    public float moveX = 0.0f;
    public float moveZ = 0.0f;
    public float moveY = 0.0f;
    public float speed = 3.0f;
    Vector3 move;
    Rigidbody rb;

    private void Awake()
    {
        Rigidbody rb=GetComponent<Rigidbody>();
    }
    void Update()
    {
        move.Set(moveX, moveY, moveZ);
        transform.Translate(move * speed*Time.deltaTime);
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.transform.tag == collisionTag)
        {
            moveX = -moveX;
            moveY = -moveY;
            moveZ = -moveZ;
        }
    }
}

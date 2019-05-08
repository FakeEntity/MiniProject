using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    float moveposx;
    float moveposz;
    float dist;
    public float speed = 3.0f;
    bool move = false;
    bool rayhit = true;
    Rigidbody rb;
    Vector3 dir;
    Vector3 pos;
    public GameObject playerRays;
    public GameObject playerRayLeft;
    public GameObject playerRayRight;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {

        moveposx = 0.0f;
        moveposz = 0.0f;
        pos.Set(moveposx, 0.25f, moveposz);
        Debug.Log(transform);
    }

    void FixedUpdate()
    {
        pos.Set(moveposx, 0.25f, moveposz);
        dist = Vector3.Distance(pos, rb.transform.position);
        if (rayhit==false)
        {
            pos.Set(moveposx, 0.25f, moveposz);
            dist = Vector3.Distance(pos, rb.transform.position);
            rb.velocity = dir * speed;
            if (Mathf.Abs(dist) <= 0.2)
            {
                if (move)
                {
                    dir.Set(0, 0.25f, 0);
                    rb.velocity = dir * 0;
                    move = false;
                }
            }
        }

    }
    public void Move(Transform tran)
    {
        if (!move)
        {
            moveposx = tran.position.x;
            moveposz = tran.position.z;
            dir.Set(moveposx - rb.transform.position.x, 0.25f, moveposz - rb.transform.position.z);
            playerRays.transform.rotation = Quaternion.LookRotation(dir);
            Ray ray1 = new Ray(playerRayLeft.transform.position, dir);
            RaycastHit raycastHit;
            Ray ray2 = new Ray(playerRayRight.transform.position, dir);
            Debug.DrawRay(playerRayLeft.transform.position, dir, Color.black, 3.0f);
            Debug.DrawRay(playerRayRight.transform.position, dir, Color.black, 3.0f);
            if (Physics.Raycast(ray1, out raycastHit) && Physics.Raycast(ray2, out raycastHit))
            {
                if (raycastHit.transform.tag == "Walls")
                {
                    rayhit = true;
                    Debug.Log(rayhit);
                }
                else
                {
                    rayhit = false;
                    Debug.Log(rayhit);
                    move = true;
                }



                Debug.Log(rb.velocity);
            }
        }
    }
    void OnCollisionEnter(Collision other)
        {
            if (other.collider.tag == "Enemy")
            {
                Destroy(gameObject);
            }

        }
    }
    




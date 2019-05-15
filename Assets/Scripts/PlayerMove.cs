﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    float moveposx;
    public string collisionTag="Walls";
    float moveposz;
    float dist;
    float distb;
    public float speed = 3.0f;
    bool move = false;
    bool rayhit = true;
    Rigidbody rb;
    Vector3 dir;
    Vector3 pos;
    Vector3 posb;
    public LayerMask raylayer;
    public GameObject playerRays;
    public GameObject playerRayLeft;
    public GameObject playerRayRight;
    public GameObject gameMain;
    bool phys;
    bool physb;
    bool win;
    Swipe swipe;
    public bool findnewignore = false;



    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        swipe = gameMain.GetComponent<Swipe>();
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
        
        if (rayhit==false)
        {
            posb.Set(moveposx, 0.1f, moveposz);
            distb = Vector3.Distance(pos, rb.transform.position);
            rb.velocity = dir * speed;
            if (Mathf.Abs(distb) <= 0.2)
            {
                if (move)
                {
                    dir.Set(0, 0.25f, 0);
                    rb.velocity = dir * 0;
                    move = false;
                    if (swipe != null)
                    {
                        swipe.canswipe=true;
                        Debug.Log("canswipe: " + swipe.canswipe);
                    }
                    if (win == true)
                         ManagerScript.Instance.win= true;
                }
            }
        }

    }
    public void Move(Vector3 tran,bool Win)
    {
        if (!move)
        {       
            moveposx = tran.x;
            moveposz = tran.z;
            dir.Set(moveposx - rb.transform.position.x, 0.1f, moveposz - rb.transform.position.z);
            pos.Set(moveposx, 0.1f, moveposz);
            dist = Vector3.Distance(pos, rb.transform.position);
            playerRays.transform.rotation = Quaternion.LookRotation(dir);
            Ray ray;          
            ray = new Ray(playerRayLeft.transform.position, dir); //first ray check
            RaycastHit raycastHit;
            if (Physics.Raycast(ray, out raycastHit, dist,raylayer))
                phys = true;
            else
                phys = false;
            ray = new Ray(playerRayRight.transform.position, dir); //second ray check
            if (Physics.Raycast(ray, out raycastHit, dist, raylayer))
                    physb = true;
                else
                    physb = false;


            Debug.Log("Phys: " + phys);
            Debug.Log("Physb: " + physb);
            Debug.Log("Dist: " + dist);
            Debug.DrawRay(playerRayLeft.transform.position, dir, Color.black, 3.0f);
            Debug.DrawRay(playerRayRight.transform.position, dir, Color.black, 3.0f);


            if ((phys == true) || (physb == true)) //combining ray collision results
            {
                rayhit = true;
                Debug.Log(rayhit);
            }
            else
            {
                rayhit = false;
                Debug.Log("Rayhit: "+rayhit);
                win = Win;
                if (findnewignore)
                {
                    MoveTarget ignorePrev = swipe.ignoreObjprev.GetComponent<MoveTarget>();
                    ignorePrev.ignore = false;
                }
                MoveTarget ignore = swipe.ignoreObj.GetComponent<MoveTarget>();
                ignore.ignore=true;
                findnewignore = true;
                swipe.findnewignore = true;
                move = true;
                if (swipe != null)
                {
                    swipe.canswipe=false;
                }
            }
        }  
     }




}
    




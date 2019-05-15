using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipe : MonoBehaviour
{
    public GameObject player;
    Rigidbody playerbody;
    public float deadzone=125.0f;
    private bool tap;
    bool isDragging = false;
    Vector2 startTouch, swipeDelta;
    Vector3 swipeDeltav3;
    [HideInInspector] public bool win;
    bool firstCheck;
    public string MoveTargetTag = "MoveTarget";
    bool mouse;
    float closestX;
    float closestZ;
    #region Start Inside MoveTarget
    public GameObject ignoreObj = null;
    [HideInInspector] public GameObject ignoreObjprev = null;
    public bool findnewignore = false;
    #endregion
    Vector3 playerpos;
    Vector3 pos;
    Vector3 movepos;
    float dist;
    bool foundTarget;
    bool isMouseheld = false;
    [HideInInspector] public bool canswipe=true;
    PlayerMove playermove;

    private void Awake()
    {
        playerbody = player.GetComponent<Rigidbody>();
        playermove = player.GetComponent<PlayerMove>();
    }
    private void Start()
    {
        mouse=ManagerScript.Instance.mouse;
    }

    private void Update()
    {
        playerpos.Set (playerbody.transform.position.x,0.25f, playerbody.transform.position.z);
        tap=false;

        #region Mouse Inputs
        if (Input.GetMouseButton(0) && mouse)
        {
            isMouseheld = true;
            isDragging = true;
        }
        else
        {
            isMouseheld = false;
        }

        if (Input.GetMouseButtonDown(0) && mouse)
        {        
            tap = true;
            Debug.Log("Tap: " + tap);
            isDragging = true;
            startTouch = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0) && mouse)
        {
            Reset();
        }
        #endregion

        #region Touch Inputs
        if (Input.touches.Length != 0 && !mouse)
        {
            if (Input.touches[0].phase==TouchPhase.Began)
            {
                tap=true;
                isDragging = true;
                startTouch = Input.touches[0].position;
            }
            else if ((Input.touches[0].phase==TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled) && !mouse)
            {
                Reset();
            }
        }
        #endregion
        Debug.Log("isDragging: " + isDragging);
        Debug.Log("canSwipe: " + canswipe);
        if (isDragging && canswipe)
        {
            if (Input.touches.Length > 0 && !mouse)
            {
                Debug.Log("test");
                swipeDelta = Input.touches[0].position - startTouch;
                swipeDeltav3 = (Vector3)swipeDelta;
                Debug.Log("swipeDeltav3: " + swipeDeltav3.x + swipeDeltav3.y + swipeDeltav3.z);
                Debug.DrawRay((Vector3)startTouch, swipeDelta);
            }
            else if (isMouseheld && mouse)
            {
                Debug.Log("test");
                swipeDelta = (Vector2)Input.mousePosition - startTouch;
                swipeDeltav3 = (Vector3)swipeDelta;
                Debug.Log("swipeDeltav3: " + swipeDeltav3.x + swipeDeltav3.y + swipeDeltav3.z);
                Debug.DrawRay((Vector3)startTouch, swipeDelta);;
            }
            
        }

        //Player movement
        if (swipeDeltav3.magnitude > deadzone)
        {
            Debug.Log("test2");
            Ray raycast;
            raycast = new Ray(playerpos, swipeDeltav3);
            Debug.DrawRay(playerpos, swipeDelta, Color.black, 2.0f);
            RaycastHit[] Hits;
            firstCheck = true;
            Hits = Physics.RaycastAll(raycast, 50.0f);
            for (int i = 0; i < Hits.Length; i++)
            {
                if (Hits[i].collider.transform.tag == MoveTargetTag)
                {
                    MoveTarget mov = Hits[i].collider.GetComponent<MoveTarget>();
                    if (!mov.ignore)
                    {
                        if (firstCheck)
                        {
                            MoveTarget m_movetarget = Hits[i].collider.GetComponent<MoveTarget>();
                            win = m_movetarget.win;
                            closestX = Hits[i].collider.transform.position.x;
                            closestZ = Hits[i].collider.transform.position.z;
                            if (!findnewignore)
                            {
                                ignoreObj = Hits[i].collider.gameObject;
                            }
                            else
                            {
                                ignoreObjprev = ignoreObj;
                                ignoreObj = Hits[i].collider.gameObject;
                            }
                            pos.Set(Hits[i].collider.transform.position.x, 0.25f, Hits[i].collider.transform.position.z);
                            dist = Vector3.Distance(playerpos, pos);
                            firstCheck = false;
                            foundTarget = true;
                            Debug.Log("FoundTarget");
                        }
                        else
                        {
                            pos.Set(Hits[i].transform.position.x, 0.25f, Hits[i].transform.position.z);
                            if (Vector2.Distance(playerpos, pos) < dist)
                            {
                                ignoreObj = Hits[i].collider.gameObject;
                                MoveTarget m_movetarget = Hits[i].collider.GetComponent<MoveTarget>();
                                win = m_movetarget.win;
                                closestX = Hits[i].transform.position.x;
                                closestZ = Hits[i].transform.position.z;
                            }
                        }
                    }
                }
            }
            if (foundTarget)
            {
                movepos.Set(closestX, 0.25f, closestZ);
                playermove.Move(movepos, win);
                Debug.Log("Go");
            }
            Reset();
        }


    }
    void Reset()
    {
        isDragging = false;
        startTouch = swipeDelta = Vector2.zero;
        swipeDeltav3 = playerpos = Vector3.zero;
    }
}

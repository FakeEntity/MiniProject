using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipe : MonoBehaviour
{
    public GameObject player;
    public LayerMask moveLayer;
    Rigidbody playerbody;
    public float deadzone = 125.0f;
    private bool tap;
    bool isDragging = false;
    [HideInInspector] public Vector3 startTouch, swipeDelta, currentTouch;
    [HideInInspector] public bool win;
    [HideInInspector] public bool firstCheck = false;
    public string MoveTargetTag = "MoveTarget";
    bool mouse;
    float closestX;
    float closestZ;
    #region Start Inside MoveTarget
    public GameObject ignoreObj = null;
    [HideInInspector] public GameObject ignoreObjprev = null;
    #endregion
    [HideInInspector] public Vector3 playerpos;
    Vector3 pos;
    Vector3 movepos;
    float dist;
    bool foundTarget;
    bool isMouseheld = false;
    public bool canswipe=true;
    //bool atTarget;
    PlayerMove playermove;
    Ray ray;
    RaycastHit raycasthit;

    private void Awake()
    {
        playerbody = player.GetComponent<Rigidbody>();
        playermove = player.GetComponent<PlayerMove>();
    }
    private void Start()
    {
        //atTarget = playermove.atTarget;
        mouse=ManagerScript.Instance.mouse;
    }

    private void Update()
    {
        if (player != null)
        {
            playerpos.Set(playerbody.transform.position.x, 0.25f, playerbody.transform.position.z);
        }
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
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out raycasthit, Mathf.Infinity, moveLayer))
            {
                startTouch.Set(raycasthit.point.x, 0.25f, raycasthit.point.z);
            }
        }
        else if (Input.GetMouseButtonUp(0) && mouse)
        {
            Reset();
        }
        #endregion

        #region Touch Inputs
        if (Input.touches.Length != 0 && !mouse)
        {
            if (Input.touches[0].phase==TouchPhase.Began && canswipe)
            {
                tap=true;
                isDragging = true;
                ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
                if (Physics.Raycast(ray, out raycasthit, Mathf.Infinity, moveLayer))
                {
                    startTouch.Set(raycasthit.point.x, 0.25f, raycasthit.point.z);
                }
            }
            else if ((Input.touches[0].phase==TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled) && !mouse)
            {
                Reset();
            }
        }
        #endregion
        Debug.Log("isDragging: " + isDragging);
        Debug.Log("canSwipe: " + canswipe);
        if (isDragging)
        {
            if (Input.touches.Length > 0 && !mouse)
            {
                Debug.Log("test");
                ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
                if (Physics.Raycast(ray, out raycasthit, Mathf.Infinity, moveLayer))
                {
                    currentTouch.Set(raycasthit.point.x, 0.25f, raycasthit.point.z);
                }
                swipeDelta = currentTouch - startTouch;
                Debug.DrawRay(startTouch, swipeDelta);
            }
            else if (isMouseheld && mouse)
            {
                Debug.Log("test");
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out raycasthit, Mathf.Infinity, moveLayer))
                {
                    currentTouch.Set(raycasthit.point.x, 0.25f, raycasthit.point.z);
                }
                swipeDelta = currentTouch - startTouch;
                Debug.Log("swipeDelta: " + swipeDelta.x + swipeDelta.y + swipeDelta.z);
                Debug.DrawRay(startTouch, swipeDelta);;
            }
            
        }

        //Player movement
        if (swipeDelta.magnitude > deadzone && canswipe)
        {
            Debug.Log("test2");
            Ray raycast;
            raycast = new Ray(playerpos, swipeDelta);
            Debug.DrawRay(playerpos, swipeDelta, Color.black, 2.0f);
            RaycastHit[] Hits;
            firstCheck = true;
            Hits = Physics.RaycastAll(raycast, 50.0f);
            for (int i = 0; i < Hits.Length; i++)
            {
                if (Hits[i].collider.transform.tag == MoveTargetTag)
                {
                        if (firstCheck)
                        {
                            MoveTarget m_movetarget = Hits[i].collider.GetComponent<MoveTarget>();
                            win = m_movetarget.win;
                            closestX = Hits[i].collider.transform.position.x;
                            closestZ = Hits[i].collider.transform.position.z;
                            pos.Set(Hits[i].collider.transform.position.x, 0.25f, Hits[i].collider.transform.position.z);
                            dist = Vector3.Distance(playerpos, pos);
                            firstCheck = false;
                            foundTarget = true;
                            //ignoreObj = Hits[i].collider.gameObject;
                            Debug.Log("FoundTarget");
                        }
                        else
                        {
                            pos.Set(Hits[i].collider.transform.position.x, 0.25f, Hits[i].collider.transform.position.z);
                            if (Vector3.Distance(playerpos, pos) < dist)
                            {
                                MoveTarget m_movetarget = Hits[i].collider.GetComponent<MoveTarget>();
                                win = m_movetarget.win;
                                closestX = Hits[i].collider.transform.position.x;
                                closestZ = Hits[i].collider.transform.position.z;
                                //ignoreObj = Hits[i].collider.gameObject;
                            }
                        }
                    
                }
            }
            if (foundTarget)
            {
                movepos.Set(closestX, 0.25f, closestZ);
                playermove.Move(movepos, win);
                foundTarget = false;
                Debug.Log("Go");
            }
            //Reset();
        }


    }
    void Reset()
    {
        isDragging = false;
        startTouch = currentTouch;
        swipeDelta = Vector3.zero;

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Touches : MonoBehaviour
{
    public GameObject player;
    public GameObject playerCol;
    public LayerMask moveLayer;
    Camera cam;
    public string MoveTargetTag = "MoveTarget";
    string colName;
    [HideInInspector] public Vector3 swipeStart;
    Vector3 swipeDelta;
    Vector3 swipeEnd;
    Vector3 pos;
    Vector3 movepos;
    PlayerMove m_playermove;
    float dist;
    [HideInInspector] public bool isPlayer = false;
    bool foundTarget = false;
    bool win=false;
    [HideInInspector] public bool canswipe = true;
    bool touch = false;
    bool isTouch = false;
    public float deadzone=50.0f;
    float closestX, closestZ;
    public GameObject ignoreObj = null;
    [HideInInspector] public GameObject ignoreObjprev = null;
    public bool findnewignore = false;

    void Awake()
    {
        m_playermove = player.GetComponent<PlayerMove>();
    }

    void Start()
    {
        cam = Camera.main;
        colName = playerCol.name;
    }

    void Update()
    {
        if (canswipe)
        {
            //if (Input.GetTouch(0).phase == TouchPhase.Ended || (Input.GetTouch(0).phase == TouchPhase.Canceled))
            //{ isTouch = false; }
            if (Input.GetMouseButtonUp(0))
            {
                isTouch = false;
                Debug.Log("IsTouch: " + isTouch);
            }
            if (Input.GetMouseButtonDown(0))
            {
                isTouch = true;
                touch = true;
                Debug.Log("IsTouch: " + isTouch);
            }
                  //Vector3 mousePositionOnScreen = Input.mousePosition;
                //Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (touch)
            {
                if (isTouch==false)
                {
                    if (isPlayer)
                    {
                        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                        RaycastHit raycasthit;
                        if (Physics.Raycast(ray, out raycasthit, Mathf.Infinity, moveLayer))
                        {
                            swipeEnd.Set(raycasthit.point.x,0.25f, raycasthit.point.z);
                        }
                        //swapEnd = Input.touches[0].position;
                        //swipeEnd.Set (Camera.main.ScreenToWorldPoint(hhh));
                        Debug.Log("mouseposx: " + Input.mousePosition.x);
                        Debug.Log("mouseposy: " + Input.mousePosition.y);
                        swipeDelta = swipeEnd - swipeStart;
                        Debug.Log("SwipeStart: " + swipeStart);
                        Debug.Log("SwipeEnd: " + swipeEnd);
                        Debug.Log("Delta: " + swipeDelta);
                        Debug.Log("SwipeDelta: " + swipeDelta.magnitude);
                    }
                    touch = false;
                }
            }

            if (isPlayer && swipeDelta.magnitude > deadzone)
            {
                Ray raycast;
                raycast = new Ray(swipeStart, swipeDelta);
                Debug.DrawRay(swipeStart, swipeDelta, Color.black, 2.0f);
                RaycastHit[] Hits;
                bool firstCheck = true;
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
                                dist = Vector2.Distance(swipeStart, pos);
                                firstCheck = false;
                                foundTarget = true;
                                Debug.Log("FoundTarget");
                            }
                            else
                            {
                                pos.Set(Hits[i].transform.position.x, 0.25f, Hits[i].transform.position.z);
                                if (Vector2.Distance(swipeStart, pos) < dist)
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
                    m_playermove.Move(movepos, win);
                    Debug.Log("Go");
                }
                Reset();
            }
        }
    }
    public void swap(bool cswap)
    {
        canswipe=cswap;
    }
    private void Reset()
    {
        foundTarget = isPlayer = win = false;
        swipeStart = swipeEnd= swipeDelta = Vector3.zero;
    }
}

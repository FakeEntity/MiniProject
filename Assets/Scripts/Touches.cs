using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Touches : MonoBehaviour
{
    public GameObject player;
    public GameObject playerCol;
    public GameObject restartButton;
    public GameObject quitButton;
    public string MoveTargetTag = "MoveTarget";
    string colName;
    Vector2 swapStart;
    Vector2 swapDelta;
    Vector2 swapEnd;
    Vector2 pos;
    Vector3 movepos;
    PlayerMove m_playermove;
    float dist;
    bool isPlayer = false;
    bool isRestart = false;
    bool isQuit = false;
    bool foundTarget = false;
    bool win=false;
    bool canswap = true;
    public float deadzone=50.0f;
    float closestX, closestZ;

    void Awake()
    {
        PlayerMove m_playermove = player.GetComponent<PlayerMove>();
    }

    void Start()
    {
        colName = playerCol.name;
    }

    void Update()
    {
        if (canswap)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {

                swapStart = Input.GetTouch(0).position;
                Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit raycastHit;
                if (Physics.Raycast(raycast, out raycastHit))
                {
                    if (raycastHit.collider.name == colName)
                    {
                        isPlayer = true;
                    }
                    else
                    {
                        isPlayer = false;
                    }
                    if (raycastHit.collider.gameObject == restartButton)
                    {
                        isRestart = true;
                    }
                    if (raycastHit.collider.gameObject == quitButton)
                    {
                        isQuit = true;
                    }
                }
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Ended || (Input.GetTouch(0).phase == TouchPhase.Canceled))
            {
                if (isPlayer)
                {
                    swapEnd = Input.GetTouch(0).position;
                    swapDelta = swapEnd - swapStart;
                }

                Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit raycastHit;
                if (Physics.Raycast(raycast, out raycastHit))
                {
                    if ((isQuit) && (raycastHit.collider.gameObject == quitButton))
                    {
                        Application.Quit();
                    }
                    if ((isRestart) && (raycastHit.collider.gameObject == restartButton))
                    {
                        ManagerScript.Instance.restartscene();
                    }
                }
                
            }

            if (isPlayer && swapDelta.magnitude > deadzone)
            {
                Ray raycast;
                raycast = new Ray(swapStart, swapEnd);
                Debug.DrawRay(swapStart, swapEnd, Color.black, 2.0f);
                RaycastHit[] Hits;
                bool firstCheck = true;
                Hits = Physics.RaycastAll(raycast, swapDelta.magnitude);
                for (int i = 0; i < Hits.Length; i++)
                {
                    if (Hits[i].transform.tag == MoveTargetTag)
                    {
                        if (firstCheck)
                        {
                            MoveTarget m_movetarget = Hits[i].collider.GetComponent<MoveTarget>();
                            win = m_movetarget.win;
                            closestX = Hits[i].transform.position.x;
                            closestZ = Hits[i].transform.position.z;
                            pos.Set(Hits[i].transform.position.x, Hits[i].transform.position.z);
                            dist = Vector2.Distance(swapStart, pos);
                            firstCheck = false;
                            foundTarget = true;
                        }
                        else
                        {
                            pos.Set(Hits[i].transform.position.x, Hits[i].transform.position.z);
                            if (Vector2.Distance(swapStart, pos) < dist)
                            {
                                MoveTarget m_movetarget = Hits[i].collider.GetComponent<MoveTarget>();
                                win = m_movetarget.win;
                                closestX = Hits[i].transform.position.x;
                                closestZ = Hits[i].transform.position.z;
                            }
                        }
                    }
                }
                if (foundTarget)
                {
                    movepos.Set(closestX, 0.25f, closestZ);
                    m_playermove.Move(movepos, win);
                }
                Reset();
            }
        }
    }
    public void swap(bool cswap)
    {
        canswap=cswap;
    }
    private void Reset()
    {
        foundTarget = isPlayer = isRestart = win = false;
    }
}

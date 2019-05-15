using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTarget : MonoBehaviour
{
    PlayerMove m_playermove;
    public GameObject player;
    public bool win=false;
    public bool ignore = false;
    Vector3 pos;
    BoxCollider boxcol;

    void Awake()
    {
        boxcol = GetComponent<BoxCollider>();
        m_playermove = player.GetComponent<PlayerMove>();

    }
    private void Start()
    {
        pos.Set(transform.position.x,0.25f,transform.position.z);
    }
    private void Update()
    {
        if (ignore)
        {
            boxcol.enabled = false;
        }
        else
        {
            boxcol.enabled=true;
        }
    }

    //void OnMouseDown()
    //{      
    //    if (!ManagerScript.Instance.mouse)
    //        m_playermove.Move(pos,win);
    //}
}

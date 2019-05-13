using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTarget : MonoBehaviour
{
    PlayerMove m_playermove;
    public GameObject player;
    public bool win=false;
    Vector3 pos;

    void Awake()
    {
        m_playermove = player.GetComponent<PlayerMove>();

    }
    private void Start()
    {
        pos.Set(transform.position.x,0.25f,transform.position.z);
    }

    void OnMouseDown()
    {      
            m_playermove.Move(pos,win);
    }
}

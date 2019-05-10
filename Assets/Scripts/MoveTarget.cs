using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTarget : MonoBehaviour
{
    PlayerMove m_playermove;
    public GameObject player;
    public bool win=false;

    void Awake()
    {
        m_playermove = player.GetComponent<PlayerMove>();

    }

    void OnMouseDown()
    {      
            m_playermove.Move(transform,win);
    }
}

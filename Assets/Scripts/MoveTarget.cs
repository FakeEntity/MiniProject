using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTarget : MonoBehaviour
{
    PlayerMove m_playermove;
    public GameObject player;

    void Awake()
    {
        m_playermove = player.GetComponent<PlayerMove>();

    }

    void OnMouseDown()
    {      
            m_playermove.Move(this.transform);
    }
}

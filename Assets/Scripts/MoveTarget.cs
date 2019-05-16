using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTarget : MonoBehaviour
{
    PlayerMove m_playermove;
    public GameObject player;
    public bool finish = false;
    [HideInInspector] public bool win = false;
    public Sprite winsprite;
    Vector3 pos;
    BoxCollider boxcol;
    public GameObject gameMain;
    GameScript gameScript;
    SpriteRenderer spriteRender;

    void Awake()
    {
        gameScript = gameMain.GetComponent<GameScript>();
        boxcol = GetComponent<BoxCollider>();
        m_playermove = player.GetComponent<PlayerMove>();
        spriteRender = GetComponent<SpriteRenderer>();

    }
    private void Start()
    {
        pos.Set(transform.position.x,0.25f,transform.position.z);
    }
    private void Update()
    {
        if (finish)
        {
            Debug.Log("gScore: " + gameScript.score);
            Debug.Log("gmaxScore: " + gameScript.maxscore);
            if (gameScript.score >= gameScript.maxscore)
            {
                win=true;
                spriteRender.sprite=winsprite;
            }
        }
    }

    //void OnMouseDown()
    //{      
    //    if (!ManagerScript.Instance.mouse)
    //        m_playermove.Move(pos,win);
    //}
}

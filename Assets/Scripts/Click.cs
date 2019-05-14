using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour
{
    Touches touches;
    public GameObject gameMain;
    public GameObject playerMain;
    Rigidbody rb;
    Rigidbody playerrb;

    void Awake()
    {
        touches = gameMain.GetComponent<Touches>();
        rb = GetComponent<Rigidbody>();
        playerrb = playerMain.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.transform.position=playerrb.transform.position;
    }

    private void OnMouseDown()
    {
        Debug.Log("canswipe: " + touches.canswipe);

        if (touches.canswipe)
        {
            touches.isPlayer = true;
            Debug.Log("IsPlayer: " + touches.isPlayer);
            touches.swipeStart.Set(rb.transform.position.x, 0.25f, rb.transform.position.z);
        }

    }
}

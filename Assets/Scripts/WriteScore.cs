using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WriteScore : MonoBehaviour
{
    TextMeshProUGUI texty;
    public GameObject gameMain;
    GameScript gameScript;
    private void Awake()
    {
        gameScript = gameMain.GetComponent<GameScript>();
        texty = GetComponent<TextMeshProUGUI>();
    }
    void Update()
    {
        texty.text=gameScript.score + " / " + gameScript.maxscore;
    }

}

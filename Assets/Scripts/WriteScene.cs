using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class WriteScene : MonoBehaviour
{
    TextMeshProUGUI texty;
    private void Awake()
    {
        texty = GetComponent<TextMeshProUGUI>();
    }
    void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        texty.text =scene.name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

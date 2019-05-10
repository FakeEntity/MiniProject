using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerScript : MonoBehaviour
{
    public static ManagerScript Instance { get; private set; }

    [HideInInspector] public bool win = false;
    public int currentScene = 0;
    bool keypressR;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }
    void Update()
    {
        keypressR = Input.GetButtonDown("Restart");
        if (keypressR)
        {
                win = false;
                SceneManager.LoadSceneAsync(currentScene);
        }
    }
}

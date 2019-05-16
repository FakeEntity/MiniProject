using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScript : MonoBehaviour
{
    public bool skipFadeOut = false;
    public float fadeOutDuration = 3.0f;
    public float waitBeforeEnd = 1.0f;
    public float fadeInDuration = 3.0f;
    public CanvasGroup Imager;
    public GameObject player;
    public int NextScene=1;
    float timer;
    bool state = false;
    public bool win = false;
    int currentScene;
    public int score = 0;
    public int maxscore = 0;
    public GameObject[] scoreObj;



    private void Awake()
    {
        timer = fadeOutDuration;
        if (skipFadeOut)
        {
            state = true;
            Imager.alpha = 0;
        }
    }
    private void Start()
    {
        currentScene = ManagerScript.Instance.currentScene;
        ManagerScript.Instance.score = score;
        scoreObj = GameObject.FindGameObjectsWithTag("Score");
        maxscore = scoreObj.Length;
        Debug.Log("Maxscore: " + maxscore);
    }

    public void FadeOut(CanvasGroup image,float duration)
    {
        timer -= Time.deltaTime;
        if (timer > 0)
        {
            image.alpha = timer / duration ;
        }
        if (timer <=0)
        {
            state = true;
        }
    }
    public void End(CanvasGroup image, float duration, float wait)
    {
        player.GetComponent<PlayerMove>().enabled = false;
        timer+= Time.deltaTime;
        if (timer<duration)
            image.alpha = timer / duration;
        if (timer > duration + wait)
        {
            timer = 0;
            ManagerScript.Instance.win = false;
            ManagerScript.Instance.currentScene = NextScene;
            SceneManager.LoadSceneAsync(NextScene);
            SceneManager.UnloadSceneAsync(currentScene);
        }
    }
        void Update()
        {
        score = ManagerScript.Instance.score;
        win = ManagerScript.Instance.win;
        if (!state)
            FadeOut(Imager, fadeOutDuration);
        if (win==true)
        {
            End(Imager, fadeInDuration,waitBeforeEnd);
        }
    }
    public void RestartScene()
    {
        ManagerScript.Instance.RestartScene();
    }
    public void QuitGame()
    {
        ManagerScript.Instance.QuitGame();
    }
}


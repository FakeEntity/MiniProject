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

    private void Awake()
    {
        currentScene = ManagerScript.Instance.currentScene;
        timer = fadeOutDuration;
        if (skipFadeOut)
        {
            state = true;
            Imager.alpha = 0;
        }
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
            ManagerScript.Instance.win = false;
            SceneManager.LoadSceneAsync(NextScene);
            SceneManager.UnloadSceneAsync(currentScene);
            ManagerScript.Instance.currentScene += 1;

        }
    }
        void Update()
        {
        Debug.Log("CurrentScene: "+currentScene);

        if (!state)
            FadeOut(Imager, fadeOutDuration);
        Debug.Log(timer);
        if (ManagerScript.Instance.win==true)
        {
            End(Imager, fadeInDuration,waitBeforeEnd);
        }
    }
}


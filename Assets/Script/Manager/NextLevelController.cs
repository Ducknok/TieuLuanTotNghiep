using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NextLevelController : DucMonobehavior
{
    CanvasGroup canvasGroup;
    [SerializeField] protected FadeUI fadeUI;
    [SerializeField] protected string sceneToLoad;
    [SerializeField] protected float fadeTime;

    protected override void Start()
    {
        this.fadeUI = FindObjectOfType<FadeUI>();
        this.fadeUI.FadeUIOut(this.fadeTime);
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            StartCoroutine(FadeAndStartGame(this.sceneToLoad));
        }
    }
    
    IEnumerator FadeAndStartGame(string sceneToLoad)
    {
        this.fadeUI.FadeUIIn(fadeTime);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(sceneToLoad);
    }
}

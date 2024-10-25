using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NextLevelController : MonoBehaviour
{
    CanvasGroup canvasGroup;
    [SerializeField] protected FadeUI fadeUI;
    [SerializeField] protected string sceneToLoad;
    [SerializeField] protected float fadeTime;

    void Start()
    {
        this.fadeUI = FindObjectOfType<FadeUI>();
        this.fadeUI.FadeUIOut(this.fadeTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
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

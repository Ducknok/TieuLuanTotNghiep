using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuFadeController : DucMonobehavior
{
    [SerializeField] protected FadeUI fadeUI;
    [SerializeField] protected float fadeTime;
    // Start is called before the first frame update
    protected override void Start()
    {
        this.fadeUI = GetComponent<FadeUI>();
        this.fadeUI.FadeUIOut(this.fadeTime);
    }

    public virtual void CallFadeAndStartGame(string sceneToLoad)
    {
        StartCoroutine(FadeAndStartGame(sceneToLoad));
    }
    IEnumerator FadeAndStartGame(string sceneToLoad)
    {
        this.fadeUI.FadeUIIn(fadeTime);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(sceneToLoad);
    }
}

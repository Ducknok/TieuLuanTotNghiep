using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuFadeController : MonoBehaviour
{
    [SerializeField] protected FadeUI fadeUI;
    [SerializeField] protected float fadeTime;
    // Start is called before the first frame update
    void Start()
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
    // Update is called once per frame
    void Update()
    {
        
    }
}

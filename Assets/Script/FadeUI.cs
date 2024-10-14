using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeUI : MonoBehaviour
{
    CanvasGroup canvasGroup;
    protected virtual void Awake()
    {
        this.canvasGroup = GetComponent<CanvasGroup>();
    }
    public virtual void FadeUIOut(float  seconds)
    {
        StartCoroutine(FadeOut(seconds));
    }
    public virtual void FadeUIIn(float seconds)
    {
        StartCoroutine(FadeIn(seconds));
    }
    IEnumerator FadeOut(float seconds)
    {
        this.canvasGroup.interactable = false;
        this.canvasGroup.blocksRaycasts = false;
        this.canvasGroup.alpha = 1;
        while(this.canvasGroup.alpha > 0)
        {
            this.canvasGroup.alpha -= Time.unscaledDeltaTime / seconds;
            yield return null;
        }
        yield return null;
    }
    IEnumerator FadeIn(float seconds)
    {
        
        this.canvasGroup.alpha = 0;
        while(this.canvasGroup.alpha < 1)
        {
            this.canvasGroup.alpha += Time.unscaledDeltaTime / seconds;
            yield return null;
        }
        this.canvasGroup.interactable = true;
        this.canvasGroup.blocksRaycasts = true;
        yield return null;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

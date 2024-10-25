using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarCutFallDown : MonoBehaviour
{
    [SerializeField] protected RectTransform rectTransform;
    [SerializeField] protected Image image;
    [SerializeField] protected Color color;
    [SerializeField] protected float fallDownTimer;
    [SerializeField] protected float fadeTimer;
    [SerializeField] protected virtual void Awake()
    {
        this.rectTransform = transform.GetComponent<RectTransform>();
        this.image = transform.GetComponent<Image>();
        this.color = image.color;
        this.fallDownTimer = 1f;
        this.fadeTimer = 1f;
    }
    protected virtual void Update()
    {
        this.fallDownTimer -= Time.deltaTime;
        if(this.fallDownTimer < 0)
        {
            float fallSpeed = 10f;
            this.rectTransform.anchoredPosition += Vector2.down * fallSpeed * Time.deltaTime;
            this.fadeTimer -= Time.deltaTime;
            if(this.fadeTimer < 0)
            {
                float alphaFadeSpeed = 5f;
                this.color.a -= alphaFadeSpeed * Time.deltaTime;
                this.image.color = this.color;
                if(this.color.a <= 0)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}

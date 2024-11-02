using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopup : DucMonobehavior
{
    private const float DISAPPEAR_TIMER_MAX = 1f;
    private static int sortingOrder;
    [SerializeField] protected TextMeshPro textMesh;
    [SerializeField] public Transform damageText;
    [SerializeField] protected float disappearTimer;
    private Color textColor;
    private Vector3 moveVector;
    
    

    protected override void Awake()
    {
        this.textMesh = transform.GetComponent<TextMeshPro>();
    }
    protected override void Update()
    {
        this.transform.position += this.moveVector * Time.deltaTime;
        this.moveVector -= this.moveVector * 8f * Time.deltaTime;
        this.disappearTimer -= Time.deltaTime;

        if(this.disappearTimer > DISAPPEAR_TIMER_MAX * 0.5f)
        {
            float increaseScaleAmount = 1f;
            this.transform.localScale += Vector3.one * increaseScaleAmount * Time.deltaTime;
        }
        else
        {
            float decreaseScaleAmount = 1f;
            this.transform.localScale -= Vector3.one * decreaseScaleAmount * Time.deltaTime;
        }
        if(this.disappearTimer < 0)
        {
            float disappearSpeed = 3f;
            this.textColor.a -= disappearSpeed * Time.deltaTime;
            this.textMesh.color = textColor;
            
            if(textColor.a < 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
    public void Setup(float damageAmount, bool isCriticalHit)
    {
        this.textMesh.SetText(damageAmount.ToString());
        if (!isCriticalHit)
        {
            this.textMesh.fontSize = 2f;
            this.textColor = Color.white;
        }
        else
        {
            this.textMesh.fontSize = 3f;
            this.textColor = Color.yellow;
        }

        sortingOrder++;
        this.textMesh.sortingOrder = sortingOrder;
        this.textMesh.color = this.textColor;
        this.disappearTimer = DISAPPEAR_TIMER_MAX;
        this.moveVector = new Vector3(0.7f, 1) * 5f;
    }
    public DamagePopup Create(Vector3 position, float damageAmount, bool isCriticalHit)
    {
        Transform damagePopupTransform = Instantiate(this.damageText, position, Quaternion.identity);
        DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
        damagePopup.Setup(damageAmount, isCriticalHit);
        return damagePopup;
    }

}

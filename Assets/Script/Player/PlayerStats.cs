using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : DucMonobehavior
{
    [Header("Component")]
    [SerializeField] private static PlayerStats instance;
    public static PlayerStats Instance => instance; 
    [SerializeField] protected PlayerController playerCtrl;
    public PlayerController PlayerCtrl => playerCtrl;
    [SerializeField] protected GameObject
        deathChunkParticle,
        deathBloodParticle;
    [SerializeField] protected GameObject hitPariticle;
    [SerializeField] protected GameObject gameOverImage;
    [SerializeField] protected Image healthImg;
    [SerializeField] protected Image manaImg;
    [SerializeField] protected Transform healthCut;
    [SerializeField] protected Transform manaCut;
    [SerializeField] protected Animator mainMenuAC;

    [Header("Atribute")]
    [SerializeField] public float maxHealth;
    [SerializeField] public float currentHealth;
    [SerializeField] public float maxMana;
    [SerializeField] public float currentMana;
    [SerializeField] protected const float HealthBar_Width = 500f;
    [SerializeField] protected const float ManaBar_Width = 300f;
    protected override void Start()
    {
        this.maxHealth = PlayerPrefs.GetFloat("MaxHealth", maxHealth);
        this.maxMana = PlayerPrefs.GetFloat("MaxMana", maxMana);
        this.gameOverImage.SetActive(false);
        this.playerCtrl = this.transform.GetComponent<PlayerController>();
        this.currentHealth = this.maxHealth;
        this.currentMana = this.maxMana;
        this.healthImg.fillAmount = this.currentHealth / 200;
        this.manaImg.fillAmount = this.currentMana / 100;
        
    }
    protected override void Awake()
    {
        PlayerStats.instance = this;
    }
    protected override void Update()
    {
        //TODO: xu ly khi full mau, ma thi ko the su dung health, mana potion
        this.healthImg.fillAmount = currentHealth / 200;
        this.manaImg.fillAmount = this.currentMana / 100;

        if (this.currentHealth <= 0.0f)
        {
            AudioManager.Instance.PlayAudio(AudioManager.Instance.dead);
            Time.timeScale = 0;
            AudioManager.Instance.backgroundMusic.Stop();
            this.gameOverImage.SetActive(true);
            this.mainMenuAC.updateMode = AnimatorUpdateMode.UnscaledTime;
            this.Die();
        }
        if(this.currentMana <= 0.0f)
        {
            Debug.Log("Khong du mana de dung spell");
        }
    }
    public virtual void DecreaseHealth(float amount)
    {
        //Debug.Log(this.playerCtrl.PlayerCom.shieldActive);
        if (!this.playerCtrl.PlayerCom.shielded)
        {
            this.currentHealth -= amount;
            AudioManager.Instance.PlayAudio(AudioManager.Instance.hit);
            Instantiate(this.hitPariticle, this.transform.position, Quaternion.Euler(0f, 0f, 360f));
            this.healthImg.fillAmount = currentHealth / 200;
            Transform damageBar = Instantiate(this.healthCut, this.healthImg.transform);
            damageBar.gameObject.SetActive(true);
            damageBar.GetComponent<RectTransform>().anchoredPosition = new Vector2(this.healthImg.fillAmount * HealthBar_Width, damageBar.GetComponent<RectTransform>().anchoredPosition.y);
            damageBar.GetComponent<Image>().fillAmount = amount / 200;
            //Debug.Log(damageBar.GetComponent<Image>().fillAmount);
            damageBar.gameObject.AddComponent<HealthBarCutFallDown>();
        }
        
    }
    public virtual void DecreaseMana(float amount)
    {
        this.currentMana -= amount;
        this.manaImg.fillAmount = currentMana / 100;
        Transform manaUsed = Instantiate(this.manaCut, this.manaImg.transform);
        manaUsed.gameObject.SetActive(true);
        manaUsed.GetComponent<RectTransform>().anchoredPosition = new Vector2(this.manaImg.fillAmount * ManaBar_Width, manaUsed.GetComponent<RectTransform>().anchoredPosition.y);
        manaUsed.GetComponent<Image>().fillAmount = amount / 100;
        Debug.Log(manaUsed.GetComponent<Image>().fillAmount);
        manaUsed.gameObject.AddComponent<HealthBarCutFallDown>();
    }
    public virtual void Die()
    {
        Instantiate(this.deathChunkParticle, this.transform.position, this.deathChunkParticle.transform.rotation);
        Instantiate(this.deathBloodParticle, this.transform.position, this.deathBloodParticle.transform.rotation);

        this.playerCtrl.GameManager.Respawn();
        Destroy(gameObject);
    }

    public virtual void DataToSave()
    {
        DataManager.Instance.CurrentHealthData(this.maxHealth);
        this.currentHealth = PlayerPrefs.GetFloat("CurrentHealth");
        DataManager.Instance.CurrentManaData(this.maxMana);
        this.currentMana = PlayerPrefs.GetFloat("CurrentMana");
        GameData.Instance.ClearAllDataList();
        GameManagerSingleton.Instance.GetComponent<Inventory>().InventoryToData();
        GameData.Instance.Save();
    }
}

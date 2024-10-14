using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private static PlayerStats instance;
    public static PlayerStats Instance => instance; 
    [SerializeField] protected PlayerController playerCtrl;
    public PlayerController PlayerCtrl => playerCtrl;
    [SerializeField] protected GameObject
        deathChunkParticle,
        deathBloodParticle;
    [SerializeField] protected Image healthImg;
    [SerializeField] protected Image manaImg;
    [SerializeField] protected GameObject hitPariticle;
    [SerializeField] protected GameObject gameOverImage;
    [SerializeField] protected Animator mainMenuAC;
    [SerializeField] public float maxHealth;
    [SerializeField] public float currentHealth;
    [SerializeField] public float maxMana;
    [SerializeField] public float currentMana;
    protected virtual void Start()
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
    protected virtual void Awake()
    {
        PlayerStats.instance = this;
    }
    protected virtual void Update()
    {
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
        this.currentHealth -= amount;
        AudioManager.Instance.PlayAudio(AudioManager.Instance.hit);
        Instantiate(this.hitPariticle, this.transform.position, Quaternion.Euler(0f, 0f, 360f));
    }
    public virtual void DecreaseMana(float amount)
    {
        this.currentMana -= amount;
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

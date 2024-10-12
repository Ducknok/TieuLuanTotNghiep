using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [SerializeField] protected static DataManager instance;
    public static DataManager Instance => instance;

    protected void Awake()
    {
        if (instance != null) Destroy(gameObject);
        instance = this;
    }
    protected virtual void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    public virtual void MusicData(float value)
    {
        PlayerPrefs.SetFloat("MusicVolume", value);
    }
    public virtual void SFXData(float value)
    {
        PlayerPrefs.SetFloat("SFXVolume", value);
    }
    public virtual void GoldDAta(float value)
    {
        PlayerPrefs.SetFloat("Gold", value);
    }
    public virtual void SoulData(float value)
    {
        PlayerPrefs.SetFloat("Soul", value);
    }
    public virtual void CurrentHealthData(float value)
    {
        PlayerPrefs.SetFloat("CurrentHealth", value);
    }
    public virtual void MaxHealthData(float value)
    {
        PlayerPrefs.SetFloat("MaxHealth", value);
    }
    public virtual void CurrentManaData(float value)
    {
        PlayerPrefs.SetFloat("CurrentMana", value);
    }
    public virtual void MaxManaData(float value)
    {
        PlayerPrefs.SetFloat("MaxMana", value);
    }
}

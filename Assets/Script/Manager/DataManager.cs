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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] protected AudioMixer musicMixer, effectsMixer;
    [SerializeField] public AudioSource hit, dead, backgroundMusic, gems, attack, mainMenu;

    [Header("AudioManager")]
    [SerializeField] protected static AudioManager instance;
    public static AudioManager Instance => instance;

    [Range(-80,10)]
    [SerializeField] protected float masterVol, effectVol;
    [SerializeField] protected Slider masterSlider, effectSlider;

    protected virtual void Start()
    {
        this.PlayAudio(backgroundMusic);
        this.masterSlider.value = this.masterVol;
        this.effectSlider.value = this.effectVol;

        this.masterSlider.minValue = -80;
        this.masterSlider.maxValue = 10;

        this.effectSlider.minValue = -80;
        this.effectSlider.maxValue = 10;

    }
    protected virtual void Update()
    {
        this.MasterVolume();
        this.EffectsVolume();
    }
    protected virtual void Awake()
    {
        if (instance != null) return;
        instance = this;
    }

    public virtual void MasterVolume()
    {
        this.musicMixer.SetFloat("masterVolume", this.masterSlider.value);
    }
    public virtual void EffectsVolume()
    {
        this.effectsMixer.SetFloat("effectsVolume", this.effectSlider.value);
    }
    public virtual void PlayAudio(AudioSource audio)
    {
        audio.Play();
    }
}

using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Sound : MonoBehaviour
{
    public AudioSource sfxAudio;
    public AudioClip lineclear;
    public AudioClip placeBlock;
    [SerializeField] private AudioMixer my_mixer;
    [SerializeField] private Slider slider;
    float volume_current_value;
    
    void Start()
    {
        volume_current_value=PlayerPrefs.GetFloat("Volume",1.0f);
        my_mixer.SetFloat("sfx",volume_current_value);
        if(slider!=null)
        {
            slider.value=Mathf.Pow(10,volume_current_value/20);
        }
    }
    public void play_sfx(AudioClip audio)
    {
        sfxAudio.clip=audio;
        sfxAudio.PlayOneShot(audio);
    }
    public void SetVolumesfx()
    {
       float sfxvolume= Mathf.Clamp(slider.value,0.00001f,20f);
       volume_current_value=Mathf.Log10(sfxvolume)*20;
       my_mixer.SetFloat("sfx",volume_current_value);
       PlayerPrefs.SetFloat("Volume",volume_current_value);
    }
}

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
    public void play_sfx(AudioClip audio)
    {
        sfxAudio.clip=audio;
        sfxAudio.PlayOneShot(audio);
    }
    public void SetVolumesfx()
    {
       float sfxvolume= Mathf.Clamp(slider.value,0.00001f,20f);
       my_mixer.SetFloat("sfx",Mathf.Log10(sfxvolume)*20);
    }
}

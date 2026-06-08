using UnityEngine;

public class Sound : MonoBehaviour
{
    public AudioSource sfxAudio;
    public AudioClip lineclear;
    public AudioClip placeBlock;
    public void play_sfx(AudioClip audio)
    {
        sfxAudio.clip=audio;
        sfxAudio.PlayOneShot(audio);
    }
}

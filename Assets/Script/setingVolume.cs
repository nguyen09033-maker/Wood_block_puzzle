using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class setingVolume : MonoBehaviour
{
    [SerializeField] private AudioMixer my_Mixer;
    [SerializeField] private Slider sfx_Slider;
    public void SetSFX()
    {
        float sfx_Volume=sfx_Slider.value;
        my_Mixer.SetFloat("sfx",sfx_Volume);
    }
}

using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider musicSlider;
    public Slider sfxSlider;
    public Toggle muteToggle;

    const float MIN = 0.0001f;

    void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat("MusicVol", 1f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVol", 1f);
        muteToggle.isOn = PlayerPrefs.GetInt("Muted", 0) == 1;

        ApplyMusic(musicSlider.value);
        ApplySFX(sfxSlider.value);
        ApplyMute(muteToggle.isOn);

        musicSlider.onValueChanged.AddListener(ApplyMusic);
        sfxSlider.onValueChanged.AddListener(ApplySFX);
        muteToggle.onValueChanged.AddListener(ApplyMute);
    }

    public void ApplyMusic(float v)
    {
        SetVolume("MusicVol", v);
        PlayerPrefs.SetFloat("MusicVol", v);
    }

    public void ApplySFX(float v)
    {
        SetVolume("SFXVol", v);
        PlayerPrefs.SetFloat("SFXVol", v);
    }

    public void ApplyMute(bool on)
    {
        if (on)
        {
            mixer.SetFloat("MasterVol", -80f);
        }
        else 
        {
            mixer.SetFloat("MasterVol", 0f);
        }
        PlayerPrefs.SetInt("Muted", on ? 1 : 0);
    }

    void SetVolume(string param, float v)
    {
        float value = Mathf.Clamp(v, MIN, 1f);
        float dB = Mathf.Log10(value) * 20f;
        mixer.SetFloat(param, dB);
    }
}

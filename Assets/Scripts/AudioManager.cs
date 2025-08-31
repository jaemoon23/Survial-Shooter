using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider musicSlider;
    public Slider sfxSlider;
    public Toggle muteToggle;   

    const float MIN = 0.0001f;  // log10(0)�� -���Ѵ��̹Ƿ� 0�� �ƴ� ���� ���� ��

    void Start()
    {
        // ������Ʈ������ ���� �ҷ�����
        musicSlider.value = PlayerPrefs.GetFloat("MusicVol", 1f);   // ���� ã�� ���ϸ� �⺻�� 1
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVol", 1f);       // ���� ã�� ���ϸ� �⺻�� 1
        muteToggle.isOn = PlayerPrefs.GetInt("Muted", 0) == 1;      // ���� ã�� ���ϸ� �⺻�� false(0)

        // �ҷ��� ���� ����� �ͼ��� ����
        ApplyMusic(musicSlider.value);
        ApplySFX(sfxSlider.value);
        ApplyMute(muteToggle.isOn);


        // UI �̺�Ʈ�� �Լ� ����
        // onValueChanged >> UI ������Ʈ ���� ����� �� �߻��ϴ� �̺�Ʈ
        // AddListener >> �̺�Ʈ�� ���� �� �� ȣ���� �޼���
        musicSlider.onValueChanged.AddListener(ApplyMusic);
        sfxSlider.onValueChanged.AddListener(ApplySFX);
        muteToggle.onValueChanged.AddListener(ApplyMute);
    }

    public void ApplyMusic(float musicVolume)
    {
        SetVolume("MusicVol", musicVolume);             // ����� �ͼ��� ���� ����
        PlayerPrefs.SetFloat("MusicVol", musicVolume);  // ������Ʈ���� ���� ����
    }

    public void ApplySFX(float sfxVolume)
    {
        SetVolume("SFXVol", sfxVolume);
        PlayerPrefs.SetFloat("SFXVol", sfxVolume);
    }

    public void ApplyMute(bool isMuted)
    {
        if (isMuted)
        {
            mixer.SetFloat("MasterVol", -80f);
        }
        else 
        {
            mixer.SetFloat("MasterVol", 0f);
        }
        PlayerPrefs.SetInt("Muted", isMuted ? 1 : 0);
    }

    // ������ 0~1 ������ ������ �޾Ƽ� dB�� ��ȯ�Ͽ� ����� �ͼ��� ����
    void SetVolume(string name, float volume)
    {
        float value = Mathf.Clamp(volume, MIN, 1f); // �־��� ���� MIN~1 ���̷� ����
        float dB = Mathf.Log10(value) * 20f;        // dB ��ȯ ���� dB = 20 * log10(������)
        mixer.SetFloat(name, dB);                   // name�� �ش��ϴ� �Ķ���Ϳ� dB �� ����
    }
}

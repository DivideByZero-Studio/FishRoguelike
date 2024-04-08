using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSlidersController : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private const float minSliderValue = -45f;
    private const float maxSliderValue = 0f;

    private void Awake()
    {
        var master = PlayerPrefs.GetFloat("MasterVolume", 0f);
        masterSlider.value = Mathf.InverseLerp(minSliderValue, maxSliderValue, master);

        var music = PlayerPrefs.GetFloat("MusicVolume", 0f);
        musicSlider.value = Mathf.InverseLerp(minSliderValue, maxSliderValue, music);

        var sfx = PlayerPrefs.GetFloat("SFXVolume", 0f);
        sfxSlider.value = Mathf.InverseLerp(minSliderValue, maxSliderValue, sfx);

        Debug.Log($"{master} {music} {sfx}");
    }

    private void OnEnable()
    {
        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    private void OnDisable()
    {
        masterSlider.onValueChanged.RemoveListener(SetMasterVolume);
        musicSlider.onValueChanged.RemoveListener(SetMusicVolume);
        sfxSlider.onValueChanged.RemoveListener(SetSFXVolume);
    }

    private float CalculateVolumeValue(float volume)
    {
        var volumeValue = Mathf.Lerp(minSliderValue, maxSliderValue, volume);
        if (volumeValue < minSliderValue + 5f) volumeValue = -80f;

        return volumeValue;
    }

    private void SetMasterVolume(float volume)
    {
        float volumeValue = CalculateVolumeValue(volume);
        mixer.SetFloat("MasterVolume", volumeValue);
        PlayerPrefs.SetFloat("MasterVolume", volumeValue);
        PlayerPrefs.Save();
    }

    private void SetMusicVolume(float volume)
    {
        float volumeValue = CalculateVolumeValue(volume);
        mixer.SetFloat("MusicVolume", volumeValue);
        PlayerPrefs.SetFloat("MusicVolume", volumeValue);
        PlayerPrefs.Save();
    }

    private void SetSFXVolume(float volume)
    {
        float volumeValue = CalculateVolumeValue(volume);
        mixer.SetFloat("SFXVolume", volumeValue);
        PlayerPrefs.SetFloat("SFXVolume", volumeValue);
        PlayerPrefs.Save();
    }
}

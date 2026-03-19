using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsController : MonoBehaviour
{
    [Header("Panel")]
    public GameObject settingsPanel;

    [Header("Audio Mixer")]
    public AudioMixer audioMixer;

    [Header("Sliders")]
    public Slider globalSlider;
    public Slider musicSlider;
    public Slider effectsSlider;

    void Start()
    {
        settingsPanel.SetActive(false);

        // Set sliders to current mixer values on load
        globalSlider.onValueChanged.AddListener(SetGlobalVolume);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        effectsSlider.onValueChanged.AddListener(SetEffectsVolume);

        SetGlobalVolume(globalSlider.value);
        SetMusicVolume(musicSlider.value);
        SetEffectsVolume(effectsSlider.value);
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }

    public void SetGlobalVolume(float value)
    {
        float db = value > 0 ? Mathf.Log10(value) * 20 : -80f;
        audioMixer.SetFloat("MasterVolume", db);
    }

    public void SetMusicVolume(float value)
    {
        float db = value > 0 ? Mathf.Log10(value) * 20 : -80f;
        audioMixer.SetFloat("MusicVolume", db);
    }

    public void SetEffectsVolume(float value)
    {
        float db = value > 0 ? Mathf.Log10(value) * 20 : -80f;
        audioMixer.SetFloat("EffectsVolume", db);
    }
}
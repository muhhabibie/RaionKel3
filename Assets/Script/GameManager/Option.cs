using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    public Slider volumeSlider;
    public Slider brightnessSlider;

    private void Start()
    {
        // Load saved settings
        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 1.0f);
        brightnessSlider.value = PlayerPrefs.GetFloat("Brightness", 1.0f);

        ApplyVolume(volumeSlider.value);
        ApplyBrightness(brightnessSlider.value);

        // Add listeners
        volumeSlider.onValueChanged.AddListener(ApplyVolume);
        brightnessSlider.onValueChanged.AddListener(ApplyBrightness);
    }

    public void ApplyVolume(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("Volume", volume);
    }

    public void ApplyBrightness(float brightness)
    {
        RenderSettings.ambientLight = Color.white * brightness;
        PlayerPrefs.SetFloat("Brightness", brightness);
    }
}

using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    public Slider volumeSlider;
    public Slider brightnessSlider;

    public AudioClip openOptionSound; // Tambahkan audio clip di Inspector
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        if (openOptionSound != null)
        {
            audioSource.PlayOneShot(openOptionSound);
        }

        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 1.0f);
        brightnessSlider.value = PlayerPrefs.GetFloat("Brightness", 1.0f);

        ApplyVolume(volumeSlider.value);
        ApplyBrightness(brightnessSlider.value);

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

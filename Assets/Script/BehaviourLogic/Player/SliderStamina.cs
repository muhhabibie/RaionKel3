using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public Slider staminaSlider;
    public float stamina = 100f;
    public float maxStamina = 100f;

    void Update()
    {
        // Menyusun stamina dan update slider
        staminaSlider.value = stamina / maxStamina;
    }
}

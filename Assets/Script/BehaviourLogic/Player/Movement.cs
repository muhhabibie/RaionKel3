using System.Collections;
using UnityEngine;
using UnityEngine.UI;  // Tambahkan ini untuk mengakses UI (Slider)

public class Movement : MonoBehaviour
{
    private bool isSlowedByWeb = false;
    public float moveSpeed = 150f;
    private float originalSpeed;
    public float speedBoostAmount = 20f;
    public float potionBoostAmount = 20f;
    public float gravity = -9.81f;
    private Vector3 velocity;
    public AudioSource footstepAudio;
    public PlayerInventory playerInventory;
    private bool isBoosted = false;

    public CharacterController controller;

    public Animator animator;

    // Mekanik stamina
    public float maxStamina = 100f;
    public float currentStamina;
    public float staminaDepletionRate = 10f;
    public float staminaRegenRate = 5f;

    public Slider staminaSlider;  // UI Slider untuk stamina

    void Start()
    {
        controller = GetComponent<CharacterController>();
        originalSpeed = moveSpeed;
        currentStamina = maxStamina;

        // Pastikan staminaSlider telah terhubung di Inspector
        if (staminaSlider != null)
        {
            staminaSlider.maxValue = maxStamina;  // Menyatakan nilai maksimal slider
            staminaSlider.value = currentStamina;  // Menyinkronkan dengan stamina awal
        }
    }

    public void ApplySpeedBoost(float duration)
    {
        if (!isBoosted)
        {
            StartCoroutine(SpeedBoostCoroutine(duration));
        }
    }

    private IEnumerator SpeedBoostCoroutine(float duration)
    {
        isBoosted = true;

       
            currentStamina = Mathf.Min(currentStamina + (maxStamina * 0.5f), maxStamina);
        
        moveSpeed += potionBoostAmount;
        yield return new WaitForSeconds(duration);

        moveSpeed = originalSpeed;
        isBoosted = false;
    }


    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(moveX, 0, moveZ).normalized;

        if (moveDirection.magnitude >= 0.1f && currentStamina > 0)
        {
            currentStamina -= staminaDepletionRate * Time.deltaTime;
        }
        else if (currentStamina < maxStamina)
        {
            currentStamina += staminaRegenRate * Time.deltaTime;
        }

        if (controller.isGrounded)
        {
            velocity.y = -1f;
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }

        // **ANIMASI BERJALAN**
        if (moveX > 0) // Jalan ke kanan
        {
            animator.SetBool("JalanSamping", true);
            animator.SetBool("JalanSampingKiri", false);
            animator.SetBool("isWalking", false);
        }
        else if (moveX < 0) // Jalan ke kiri
        {
            animator.SetBool("JalanSamping", false);
            animator.SetBool("JalanSampingKiri", true);
            animator.SetBool("isWalking", false);
        }
        else if (moveZ != 0) // Jalan depan/belakang
        {
            animator.SetBool("isWalking", true);
            animator.SetBool("JalanSamping", false);
            animator.SetBool("JalanSampingKiri", false);
        }
        else // Diam (Idle)
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("JalanSamping", false);
            animator.SetBool("JalanSampingKiri", false);
        }

        if (moveDirection.magnitude >= 0.1f && !footstepAudio.isPlaying)
        {
            footstepAudio.Play();  // Putar suara langkah
        }
        else if (moveDirection.magnitude < 0.1f)
        {
            footstepAudio.Stop();  // Hentikan suara saat diam
        }


        controller.Move((moveDirection * moveSpeed + velocity) * Time.deltaTime);

        if (currentStamina <= 0 && !isSlowedByWeb)
        {
            currentStamina = 0;
            moveSpeed = 0;
        }
        else if (currentStamina > 0 && moveSpeed == 0 && !isSlowedByWeb)
        {
            moveSpeed = originalSpeed;
        }

        if (staminaSlider != null)
        {
            staminaSlider.value = currentStamina;
        }
    
}

    public void ResetSpeedToOriginal()
    {
        moveSpeed = originalSpeed;
        isSlowedByWeb = false;
    }

    public float GetSpeed()
    {
        return moveSpeed;
    }

    public void SetSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
        isSlowedByWeb = true;
    }

}

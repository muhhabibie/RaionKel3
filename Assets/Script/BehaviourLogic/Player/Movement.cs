using System.Collections;
using UnityEngine;
using UnityEngine.UI;  // Tambahkan ini untuk mengakses UI (Slider)

public class Movement : MonoBehaviour
{
    public float moveSpeed = 150f;
    private float originalSpeed;
    public float speedBoostAmount = 20f;
    public float potionBoostAmount = 20f;
    public float gravity = -9.81f;
    private Vector3 velocity;

    public GameObject collisionIndicator;
    public GameObject specialCollisionIndicator;
    public Transform cameraTransform;

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

        // Stamina: mengurangi saat bergerak
        if (moveDirection.magnitude >= 0.1f && currentStamina > 0)
        {
            currentStamina -= staminaDepletionRate * Time.deltaTime;
        }
        else if (currentStamina < maxStamina)
        {
            currentStamina += staminaRegenRate * Time.deltaTime;
        }

        // Menambah gravitasi jika karakter di udara
        if (controller.isGrounded)
        {
            velocity.y = -1f;
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }

        // Update animasi pergerakan
        if (moveDirection.magnitude >= 0.1f)
        {
            if (moveZ > 0 || moveZ < 0)
            {
                animator.SetBool("isWalking", true);
                animator.SetBool("JalanSamping", false);
            }
            else if (moveX != 0 && moveZ == 0)
            {
                animator.SetBool("isWalking", false);
                animator.SetBool("JalanSamping", true);
            }

            if (!GetComponent<AudioSource>().isPlaying) 
            {
                GetComponent<AudioSource>().Play();  
            }
        }
        else
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("JalanSamping", false);
            GetComponent<AudioSource>().Stop();
        }

        controller.Move((moveDirection * moveSpeed + velocity) * Time.deltaTime);

    
        if (currentStamina <= 0)
        {
            currentStamina = 0;
            moveSpeed = 0;
        }
        else if (currentStamina > 0 && moveSpeed == 0)
        {
            moveSpeed = originalSpeed;
        }

        // Update stamina bar UI
        if (staminaSlider != null)
        {
            staminaSlider.value = currentStamina;
        }
    }

    public float GetSpeed()
    {
        return moveSpeed;
    }

    public void SetSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }
}

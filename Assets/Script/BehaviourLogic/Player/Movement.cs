using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
    public float moveSpeed;
    private float originalSpeed;
    public float speedBoostAmount = 20f;
    public float potionBoostAmount = 20f;
    public PlayerInventory playerInventory;
    private bool isBoosted = false;

    public CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        originalSpeed = moveSpeed;
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(moveX, 0, moveZ).normalized;

        if (moveDirection.magnitude >= 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
            controller.Move(moveDirection * moveSpeed * Time.deltaTime);
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
    private IEnumerator SpeedBoostCoroutine(float duration)
    {
        isBoosted = true;
        moveSpeed += potionBoostAmount;
        yield return new WaitForSeconds(duration);
        moveSpeed = 150f;
        isBoosted = false;
    }
    public void PotionSpeed()
    {
        playerInventory.UsePotion();
    }

    public void ApplySpeedBoost(float duration)
    {
        if (!isBoosted)
        {
            StartCoroutine(SpeedBoostCoroutine(duration));
        }
    }
}

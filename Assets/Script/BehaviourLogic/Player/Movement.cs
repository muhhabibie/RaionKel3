using System;
using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 150f;
    public float speedBoostAmount = 20f;
    public float potionBoostAmount = 20f;
    public GameObject collisionIndicator;
    public GameObject specialCollisionIndicator;
    public Transform cameraTransform;
    public PlayerInventory playerInventory;
    private bool isBoosted = false;

    private Vector3 moveDirection;
    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        if (collisionIndicator != null)
        {
            collisionIndicator.SetActive(false);
        }
        if (specialCollisionIndicator != null)
        {
            specialCollisionIndicator.SetActive(false);
        }
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

    private IEnumerator SpeedBoostCoroutine(float duration)
    {
        isBoosted = true;
        moveSpeed += potionBoostAmount;
        yield return new WaitForSeconds(duration);
        moveSpeed = 150f;
        isBoosted = false;
    }


    void Update()
    {

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");


        moveDirection = new Vector3(moveX, 0, moveZ).normalized;

        if (moveDirection.magnitude >= 0.1f)
        {

            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);

            controller.Move(moveDirection * moveSpeed * Time.deltaTime);
        }
    }
}
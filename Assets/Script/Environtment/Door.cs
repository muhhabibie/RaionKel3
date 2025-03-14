using UnityEngine;
using System.Collections;

public class Door : Interactable
{
    public Transform door;
    public float openAngle = 90f;
    public float openSpeed = 2f;
    public int requiredItems = 3;

    private bool isDoorOpen = false;
    public PlayerInventory playerInventory;

    private void Update()
    {
        if (playerInventory.collectedKeys >= requiredItems && !isDoorOpen)
        {
            Interact();
        }
    }

    public override void Interact()
    {
        if (!isDoorOpen && playerInventory.collectedKeys >= requiredItems)
        {
            isDoorOpen = true;
            Debug.Log("Pintu terbuka!");
            StartCoroutine(OpenDoorAnimation());
        }
        else
        {
            Debug.Log("Pintu terkunci! Kunci yang dibutuhkan: " + requiredItems);
        }
    }

    public override void UpdateUI()
    {
        Debug.Log("Update UI untuk Door dipanggil.");
    }

    private IEnumerator OpenDoorAnimation()
    {
        float currentAngle = door.rotation.eulerAngles.y;
        float targetY = currentAngle + openAngle;

        while (Mathf.Abs(door.rotation.eulerAngles.y - targetY) > 0.1f)
        {
            float newAngle = Mathf.MoveTowardsAngle(door.rotation.eulerAngles.y, targetY, openSpeed * Time.deltaTime * 100f);
            door.rotation = Quaternion.Euler(0f, newAngle, 0f);
            yield return null;
        }

        door.rotation = Quaternion.Euler(0f, targetY, 0f);
    }
}

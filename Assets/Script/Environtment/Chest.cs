using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class Chest : Interactable
{
    public Transform lid;
    public GameObject uiText;

    public PlayerInventory playerInventory;
    public Animator chestAnimator;  // Tambahkan referensi Animator

    public Item[] storedItems;

    private bool isOpened = false;
    private bool isPlayerNearby = false;
    private float openSpeed = 100f;
    private float targetRotation = -35f;
    private bool isOpening = false;

    private void Start()
    {
        if (uiText != null) uiText.SetActive(false);
    }

    public override void Interact()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E) && !isOpened)
        {
            isOpened = true;
            isOpening = true;
            GiveItemsToPlayer();
            if (uiText != null) uiText.SetActive(false);

            // Trigger animasi membuka peti
            if (chestAnimator != null)
            {
                chestAnimator.SetTrigger("OpenChest");
            }
        }
    }

    private void GiveItemsToPlayer()
    {
        Debug.Log("Jumlah item di dalam chest sebelum diambil: " + storedItems.Length);

        if (playerInventory != null && storedItems.Length > 0)
        {
            foreach (Item item in storedItems)
            {
                Debug.Log("Memberikan item: " + item.itemName + " x" + item.quantity);
                playerInventory.AddItem(item);
            }
            storedItems = new Item[0];
        }
        else
        {
            Debug.Log("Chest kosong! Tidak ada item.");
        }
    }

    public override void UpdateUI()
    {
        Debug.Log("Update UI untuk Chest dipanggil.");
    }

    private void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }

        if (isOpening && lid != null)
        {
            Quaternion targetRotationQuat = Quaternion.Euler(lid.rotation.eulerAngles.x, lid.rotation.eulerAngles.y, targetRotation);
            lid.rotation = Quaternion.RotateTowards(lid.rotation, targetRotationQuat, openSpeed * Time.deltaTime);

            if (Quaternion.Angle(lid.rotation, targetRotationQuat) < 1f)
            {
                isOpening = false;
                Debug.Log("Peti terbuka sepenuhnya!");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            if (uiText != null) uiText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            if (uiText != null) uiText.SetActive(false);
        }
    }
}

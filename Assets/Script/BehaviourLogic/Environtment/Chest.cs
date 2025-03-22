using UnityEngine;
using TMPro;

public class Chest : Interactable
{
    public GameObject uiText;
    public PlayerInventory playerInventory;
    public Animator chestAnimator;
    public Item[] storedItems;
    public AudioClip openSound;

    private AudioSource audioSource;

    private bool isPlayerNearby = false;


    private void Start()
    {
        chestAnimator = GetComponent<Animator>();
        if (uiText != null) 
            uiText.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public override void Interact()
    {
     
        
            GiveItemsToPlayer();
            if (uiText != null) uiText.SetActive(false);

            if (chestAnimator != null)
            {
                chestAnimator.SetTrigger("OpenChest");
            }

            if (openSound != null)
            {
                audioSource.PlayOneShot(openSound);
            }
        }
    

    private void GiveItemsToPlayer()
    {
        if (playerInventory != null && storedItems.Length > 0)
        {
            foreach (Item item in storedItems)
            {
                playerInventory.AddItem(item);
            }
            storedItems = new Item[0];
        }
    }

   


    public override void UpdateUI()
    {
        Debug.Log("Update UI untuk Chest dipanggil.");
    }

    private void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.F))
        {
            Interact();
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

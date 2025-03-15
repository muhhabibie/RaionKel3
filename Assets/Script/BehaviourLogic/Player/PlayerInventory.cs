using UnityEngine;
using static UnityEditor.Progress;

public class PlayerInventory : MonoBehaviour
{
    public int collectedKeys = 0;
    public int collectedPotions = 1;
    public float boostDuration = 5f;
    public int collectedRemotes = 0;

    public Movement playerMovement; // Referensi ke Movement script


    private void Start()
    {
        playerMovement = GetComponent<Movement>();
    }

    public void UsePotion()
    {
        if (collectedPotions > 0)
        {
            collectedPotions--; // Kurangi potion
            playerMovement.ApplySpeedBoost(boostDuration);
            Debug.Log("Potion digunakan! Sisa potion: " + collectedPotions);
        }
        else
        {
            Debug.Log("Tidak ada potion tersisa!");
        }
    }

    
    // Fungsi untuk menambah item ke inventaris
    public void AddItem(Item item)
    {
        if (item.itemName == "Key")
        {
            collectedKeys += item.quantity;
        }
        else if (item.itemName == "Potion")
        {
            collectedPotions += item.quantity;
        }
        else if (item.itemName == "Remote")
        {
            collectedRemotes += item.quantity;
        }

        Debug.Log("Item ditambah! " + item.itemName + " x" + item.quantity);
    }

    private void Update()
    {
         if (Input.GetKeyDown(KeyCode.F)) // Tekan "P" untuk menggunakan potion
        {
            UsePotion();
        }
    }


}

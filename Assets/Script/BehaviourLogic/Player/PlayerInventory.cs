using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int collectedKeys = 0;
    public int collectedPotions = 0;
    public int collectedRemotes = 0;
    public float boostDuration = 5f;

    public Movement playerMovement;

    private void Start()
    {
        playerMovement = GetComponent<Movement>();
        Debug.Log("PlayerInventory dimulai!");
    }


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

    public void UseRemote()
    { 

        if (collectedRemotes > 0)
        {
            collectedRemotes--; 
            Debug.Log("Gunakan Remote");

            EnemyAI enemy = FindFirstObjectByType<EnemyAI>(); 
            if (enemy != null)
            {
                Debug.Log("Menggunakan Remote! Musuh akan stun selama 5 detik.");
                enemy.StunEnemy(5f); 
            }
            else
            {
                Debug.Log("Tidak ada musuh yang ditemukan dalam scene.");
            }
        }
        else
        {
            Debug.Log("Tidak ada Remote tersisa!");
        }
    }
    public void UsePotion()
    {
        if (collectedPotions > 0)
        {
            collectedPotions--; 
            playerMovement.ApplySpeedBoost(boostDuration);
            Debug.Log("Potion digunakan! Sisa potion: " + collectedPotions);
        }
        else
        {
            Debug.Log("Tidak ada potion tersisa!");
        }
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) 
        {
            UsePotion();
        } 
     
        if (Input.GetKeyDown(KeyCode.X)) 
        {
            UseRemote();
        }
    }
}

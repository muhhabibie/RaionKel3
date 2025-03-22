using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInventory : MonoBehaviour
{
    public int collectedKeys = 0;
    public int collectedPotions = 0;
    public float boostDuration = 5f;
    public int collectedRemotes = 0;

    private Movement playerMovement;

    public TextMeshProUGUI keyText; 
    public TextMeshProUGUI potionText; 
    public TextMeshProUGUI remoteText; 

 
    public AudioClip potionSound;
    public AudioClip remoteSound;

    private AudioSource audioSource;

    public int maxKeys = 3; 

    private void Start()
    {
        playerMovement = GetComponent<Movement>();
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

    
        string sceneName = SceneManager.GetActiveScene().name;
        switch (sceneName)
        {
            case "Stage 1":
                maxKeys = 3;
                break;
            case "Stage 2":
                maxKeys = 5;
                break;
            case "Stage 3":
                maxKeys = 7;
                break;
            default:
                maxKeys = 3;
                break;
        }

        UpdateKeyUI(); 
        UpdateItemUI(); 
    }

  
    public void AddItem(Item item)
    {
        if (item.itemName == "Key")
        {
            collectedKeys += item.quantity;
            UpdateKeyUI(); 
        }
        else if (item.itemName == "Potion")
        {
            collectedPotions += item.quantity;
            UpdateItemUI(); 
        }
        else if (item.itemName == "Remote")
        {
            collectedRemotes += item.quantity;
            UpdateItemUI(); 
        }

        Debug.Log("Item ditambah! " + item.itemName + " x" + item.quantity);
    }


    public void UpdateKeyUI()
    {
        if (keyText != null)
        {
            keyText.text = "KEY " + collectedKeys + "/" + maxKeys; 
        }
    }


    public void UpdateItemUI()
    {
        if (potionText != null)
        {
            potionText.text = "POTION " + collectedPotions; 
        }

        if (remoteText != null)
        {
            remoteText.text = "REMOTE " + collectedRemotes; 
        }
    }


    public void UsePotion()
    {
        if (collectedPotions > 0)
        {
            collectedPotions--;
            playerMovement.ApplySpeedBoost(boostDuration); 
            Debug.Log("Potion digunakan! Sisa potion: " + collectedPotions);

          
            if (potionSound != null)
            {
                audioSource.PlayOneShot(potionSound);
            }

            UpdateItemUI(); 
        }
        else
        {
            Debug.Log("Tidak ada potion tersisa!");
        }
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

            
                if (remoteSound != null)
                {
                    audioSource.PlayOneShot(remoteSound);
                }
            }
            else
            {
                Debug.Log("Tidak ada musuh yang ditemukan dalam scene.");
            }

            UpdateItemUI(); 
        }
        else
        {
            Debug.Log("Tidak ada Remote tersisa!");
        }
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            UsePotion();
        }

        if (Input.GetKeyDown(KeyCode.X)) 
        {
            UseRemote();
        }
    }
}

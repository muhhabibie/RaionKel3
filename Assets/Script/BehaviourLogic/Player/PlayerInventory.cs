using TMPro;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int collectedKeys = 0;
    public int collectedPotions = 0;
    public float boostDuration = 5f;
    public int collectedRemotes = 0;

    private Movement playerMovement;

    // Referensi ke TextMeshPro untuk key counter dan item counter
    public TextMeshProUGUI keyText; // Untuk menampilkan jumlah key
    public TextMeshProUGUI potionText; // Untuk menampilkan jumlah potions
    public TextMeshProUGUI remoteText; // Untuk menampilkan jumlah remotes

    // Efek suara
    public AudioClip potionSound;
    public AudioClip remoteSound;

    private AudioSource audioSource;

    // Menambahkan variabel maxKeys untuk stage
    public int maxKeys = 3; // Default untuk Stage 1

    private void Start()
    {
        playerMovement = GetComponent<Movement>();
        audioSource = GetComponent<AudioSource>();

        // Jika tidak ada AudioSource, tambahkan secara otomatis
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        UpdateKeyUI(); // Update UI untuk Key
        UpdateItemUI(); // Update UI untuk Potion dan Remote
    }

    // Fungsi untuk menambah item ke inventaris
    public void AddItem(Item item)
    {
        if (item.itemName == "Key")
        {
            collectedKeys += item.quantity;
            UpdateKeyUI(); // Update UI untuk Key
        }
        else if (item.itemName == "Potion")
        {
            collectedPotions += item.quantity;
            UpdateItemUI(); // Update UI untuk Potion
        }
        else if (item.itemName == "Remote")
        {
            collectedRemotes += item.quantity;
            UpdateItemUI(); // Update UI untuk Remote
        }

        Debug.Log("Item ditambah! " + item.itemName + " x" + item.quantity);
    }

    // Fungsi untuk memperbarui UI jumlah Key
    public void UpdateKeyUI()
    {
        if (keyText != null)
        {
            keyText.text = "KEY " + collectedKeys + "/" + maxKeys; // Update dengan maxKeys
        }
    }

    // Fungsi untuk memperbarui UI jumlah item (Potion dan Remote)
    public void UpdateItemUI()
    {
        if (potionText != null)
        {
            potionText.text = "POTION " + collectedPotions; // Update dengan jumlah potions
        }

        if (remoteText != null)
        {
            remoteText.text = "REMOTE " + collectedRemotes; // Update dengan jumlah remotes
        }
    }

    // Fungsi untuk menggunakan Potion
    public void UsePotion()
    {
        if (collectedPotions > 0)
        {
            collectedPotions--;
            playerMovement.ApplySpeedBoost(boostDuration); // Menambahkan boost ke kecepatan
            Debug.Log("Potion digunakan! Sisa potion: " + collectedPotions);

            // Mainkan efek suara potion
            if (potionSound != null)
            {
                audioSource.PlayOneShot(potionSound);
            }

            UpdateItemUI(); // Update UI setelah menggunakan potion
        }
        else
        {
            Debug.Log("Tidak ada potion tersisa!");
        }
    }

    // Fungsi untuk menggunakan Remote
    public void UseRemote()
    {
        if (collectedRemotes > 0)
        {
            collectedRemotes--;
            Debug.Log("Gunakan Remote");

            EnemyAI enemy = FindObjectOfType<EnemyAI>(); // Cari musuh di scene
            if (enemy != null)
            {
                Debug.Log("Menggunakan Remote! Musuh akan stun selama 5 detik.");
                enemy.StunEnemy(5f); // Stun musuh selama 5 detik

                // Mainkan efek suara remote
                if (remoteSound != null)
                {
                    audioSource.PlayOneShot(remoteSound);
                }
            }
            else
            {
                Debug.Log("Tidak ada musuh yang ditemukan dalam scene.");
            }

            UpdateItemUI(); // Update UI setelah menggunakan remote
        }
        else
        {
            Debug.Log("Tidak ada Remote tersisa!");
        }
    }

    void Update()
    {
        // Fungsi untuk menangani input pengguna
        if (Input.GetKeyDown(KeyCode.F)) // Tekan "F" untuk menggunakan Potion
        {
            UsePotion();
        }

        if (Input.GetKeyDown(KeyCode.X)) // Tekan "X" untuk menggunakan Remote
        {
            UseRemote();
        }
    }
}

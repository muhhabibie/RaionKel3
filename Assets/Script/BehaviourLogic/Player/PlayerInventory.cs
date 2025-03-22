using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    public Image potionHighlight;
    public Image remoteHighlight;
    private AudioSource audioSource;

    public int maxKeys = 3;

    private enum ActiveItem { Potion, Remote }
    private ActiveItem currentActiveItem = ActiveItem.Potion;

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
        UpdateActiveItemUI();
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
    }

    public void UpdateKeyUI()
    {
        if (keyText != null)
        {
            keyText.text = "KEY " + collectedKeys + "/" + maxKeys;
        }
    }
    public void UpdateActiveItemUI()
    {
        // Highlight yang aktif
        if (currentActiveItem == ActiveItem.Potion)
        {
            potionHighlight.enabled = true;
            remoteHighlight.enabled = false;
        }
        else if (currentActiveItem == ActiveItem.Remote)
        {
            potionHighlight.enabled = false;
            remoteHighlight.enabled = true;
        }


        potionText.text = "POTION " + collectedPotions;
        remoteText.text = "REMOTE " + collectedRemotes;
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

            if (potionSound != null)
            {
                audioSource.PlayOneShot(potionSound);
            }
            UpdateItemUI();
        }
    }

    public void UseRemote()
    {
        if (collectedRemotes > 0)
        {
            collectedRemotes--;

            EnemyAI enemy = FindFirstObjectByType<EnemyAI>();
            if (enemy != null)
            {
                enemy.StunEnemy(5f);
                if (remoteSound != null)
                {
                    audioSource.PlayOneShot(remoteSound);
                }
            }
            UpdateItemUI();
        }
    }

    void Update()
    {
        // Switch item dengan tombol 1 dan 2
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentActiveItem = ActiveItem.Potion;
            UpdateActiveItemUI();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentActiveItem = ActiveItem.Remote;
            UpdateActiveItemUI();
        }

        // Gunakan item aktif dengan klik kiri
        if (Input.GetMouseButtonDown(0))
        {
            if (currentActiveItem == ActiveItem.Potion)
                UsePotion();
            else if (currentActiveItem == ActiveItem.Remote)
                UseRemote();
        }
    }
}

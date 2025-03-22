using UnityEngine;
using UnityEngine.UI;

public class LorePaper : MonoBehaviour
{
    public AudioClip loreAudio;
    private AudioSource audioSource;
    private bool playerInRange = false;
    private bool loreOpen = false;

    [Header("UI Elements")]
    public GameObject uiPrompt;      // UI Command "Tekan F untuk membaca"
    public GameObject uiLorePaper;   // UI yang menampilkan kertas/lore

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        uiPrompt.SetActive(false);
        uiLorePaper.SetActive(false);
    }

    void Update()
    {
        if (playerInRange && !loreOpen)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                OpenLore();
            }
        }

        if (loreOpen)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                CloseLore();
            }
        }
    }

    void OpenLore()
    {
        loreOpen = true;
        uiPrompt.SetActive(false);
        uiLorePaper.SetActive(true);

        audioSource.clip = loreAudio;
        audioSource.Play();
        Debug.Log("Lore dibuka & audio diputar.");
    }

    void CloseLore()
    {
        loreOpen = false;
        uiLorePaper.SetActive(false);
        audioSource.Stop();
        Debug.Log("Lore ditutup & audio dihentikan.");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            if (!loreOpen)
                uiPrompt.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            uiPrompt.SetActive(false);

            if (loreOpen)
            {
                CloseLore();
            }
        }
    }
}

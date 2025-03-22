using UnityEngine;
using UnityEngine.SceneManagement;

public class WinningCondition : MonoBehaviour
{
    public GameObject winUI;                  
    public AudioClip winSound;                 

    private AudioSource audioSource;
    private bool hasWon = false;

    private void Start()
    {
        if (winUI != null)
            winUI.SetActive(false);

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasWon)
        {
            PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();

            if (playerInventory != null && playerInventory.collectedKeys >= 7)
            {
                hasWon = true;
                Debug.Log("Selamat! Anda menang!");
                WinGame();
            }
            else
            {
                Debug.Log("Butuh 7 kunci untuk menang!");
            }
        }
    }

    void WinGame()
    {
        if (winUI != null)
            winUI.SetActive(true);

        if (winSound != null)
            audioSource.PlayOneShot(winSound);

        Time.timeScale = 0f; 
    }

  
    
}

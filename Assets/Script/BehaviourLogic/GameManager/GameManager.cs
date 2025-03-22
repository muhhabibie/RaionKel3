using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverUI;
    public AudioClip gameOverSound;

    [Header("Other UI Elements")]
    public GameObject[] otherUI; 

    private AudioSource audioSource;

    private void Start()
    {
        Time.timeScale = 1f;
        audioSource = GetComponent<AudioSource>();
        gameOverUI.SetActive(false);

        foreach (var ui in otherUI)
        {
            ui.SetActive(true);
        }
    }

    public void GameOver()
    {
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;

        foreach (var ui in otherUI)
        {
            ui.SetActive(false); // Matikan semua UI lain
        }


        if (gameOverSound != null)
        {
            audioSource.PlayOneShot(gameOverSound);
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Debug.Log("Keluar dari game...");
        SceneManager.LoadScene("Main Menu");
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverUI;
    public AudioClip gameOverSound;

    private AudioSource audioSource;

    private void Start()
    {
        Time.timeScale = 1f;
        audioSource = GetComponent<AudioSource>();
        gameOverUI.SetActive(false);
    }


    public void GameOver()
    {
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;

        // Mainkan suara game over
        if (gameOverSound != null)
        {
            audioSource.PlayOneShot(gameOverSound);
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        gameOverUI.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Debug.Log("Keluar dari game...");
        Application.Quit();
    }
}

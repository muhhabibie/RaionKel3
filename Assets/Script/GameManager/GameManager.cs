using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverUI;
    private bool isGameOver = false;

    private void Start()
    {
        gameOverUI.SetActive(false);
    }
    public void ShowGameOver()
    {
        gameOverUI.SetActive(true); 
        Time.timeScale = 0f; 
    }

    public void GameOver()
    {
        // Menampilkan UI Game Over
        if (!isGameOver)
        {
            isGameOver = true;
            gameOverUI.SetActive(true); // Menampilkan UI Game Over
            Time.timeScale = 0f; // Hentikan waktu saat game over
        }
    }
    public void RestartGame()
    {
        Time.timeScale = 1f; 
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);
        Debug.Log("mencet restart");
    }

    // Fungsi untuk quit game
    public void QuitGame()
    {
        Debug.Log("Keluar dari game...");
        Application.Quit(); // Keluar dari aplikasi
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePauseSystem : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    

    private AudioSource audioSource;
    private bool isPaused = false;
    private static GamePauseSystem instance;

    private string lastScene; 

    

    private void Start()
    {
        lastScene = SceneManager.GetActiveScene().name;
        Debug.Log("Game dimulai di: " + lastScene);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        if (pausePanel == null)
        {
            pausePanel = GameObject.Find("PausePanel");
            Debug.Log("Pause Panel ditemukan: " + (pausePanel != null));
        }

        if (pausePanel != null)
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0f;
            isPaused = true;

        }
    }

    public void ResumeGame()
    {
        if (pausePanel == null)
        {
            pausePanel = GameObject.Find("PausePanel");
        }

        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1f;
            isPaused = false;
        }
    }

    

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

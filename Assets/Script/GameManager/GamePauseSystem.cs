using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GamePauseSystem : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    private bool isPaused = false;
    private static GamePauseSystem instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            if (pausePanel == null)
            {
                pausePanel = GameObject.Find("PausePanel");
            }

            if (pausePanel != null)
            {
                DontDestroyOnLoad(pausePanel);
            }
        }
        else
        {
            Destroy(gameObject);
        }
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
            Time.timeScale = 0f;
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

    private string lastScene; // Menyimpan nama scene terakhir

    private void Start()
    {
        lastScene = SceneManager.GetActiveScene().name; // Simpan scene pertama saat game mulai
        Debug.Log("Game dimulai di: " + lastScene);
    }
}

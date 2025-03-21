using UnityEngine;
using UnityEngine.SceneManagement;
public class WINNING : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    // Update is called once per frame
    public void RestartGame()
    {
        SceneManager.LoadScene("Stage 1");
    }
}

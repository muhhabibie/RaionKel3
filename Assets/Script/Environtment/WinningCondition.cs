using UnityEngine;
using UnityEngine.SceneManagement;

public class WinningCondition : MonoBehaviour
{
    public string nextSceneName = "Main Menu"; // Atur scene tujuan setelah menang
    private bool hasWon = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasWon)
        {
            PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();

            if (playerInventory != null && playerInventory.collectedKeys >= 7)
            {
                hasWon = true;
                Debug.Log(" Selamat! Anda menang!");
                WinGame();
            }
            else
            {
                Debug.Log(" Butuh 7 kunci untuk menang!");
            }
        }
    }

    void WinGame()
    {
        SceneManager.LoadScene("Winning Scene");
    }
}

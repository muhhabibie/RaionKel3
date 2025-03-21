using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) // Pastikan Player memiliki Tag "Player"
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; // Dapatkan index scene saat ini
            int nextSceneIndex = currentSceneIndex + 1; // Scene selanjutnya
            
            if (nextSceneIndex < SceneManager.sceneCountInBuildSettings) // Cek jika masih ada scene berikutnya
            {
                SceneManager.LoadScene(nextSceneIndex); // Pindah ke scene berikutnya
            }
            else
            {
                Debug.Log("Tidak ada stage berikutnya!"); // Jika sudah di stage terakhir
            }
        }
    }
}

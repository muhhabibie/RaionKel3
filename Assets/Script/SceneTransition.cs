using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; 
            int nextSceneIndex = currentSceneIndex + 1; 
            
            if (nextSceneIndex < SceneManager.sceneCountInBuildSettings) 
            {
                SceneManager.LoadScene(nextSceneIndex); 
            }
            else
            {
                Debug.Log("Tidak ada stage berikutnya!"); 
            }
        }
    }
}

using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{   
        public void Playgame()
        {
            SceneManager.LoadSceneAsync("Gameplay Scene");
        }
}
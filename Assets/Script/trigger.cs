using UnityEngine;
using UnityEngine.SceneManagement;

public class trigger : MonoBehaviour
{
    public bool isNextStage = false;
    public int Stage = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
 
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {   
            Stage++;
            isNextStage = true;
        }
    }
    void Update()
    {
        if (isNextStage){
            isNextStage = false;
            SceneManager.LoadScene("Stage "+Stage);
        }
        if (Stage == 4){
            SceneManager.LoadScene("Finish");
        }
    }
}

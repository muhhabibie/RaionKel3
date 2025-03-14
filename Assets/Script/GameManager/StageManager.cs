using UnityEngine;

public class StageManager : MonoBehaviour
{
    public GameObject[] stages;
    private int currentStage = 0;

    void Start()
    {
        UpdateStage();
    }

    public void NextStage()
    {
        if (currentStage < stages.Length - 1)
        {
            stages[currentStage].SetActive(false); 
            currentStage++; 
            stages[currentStage].SetActive(true); 
            Debug.Log("Pindah ke Stage: " + (currentStage + 1));
        }
        else
        {
            Debug.Log("Semua stage sudah selesai!");
        }
    }

    void UpdateStage()
    {
        for (int i = 0; i < stages.Length; i++)
        {
            stages[i].SetActive(i == currentStage);
        }
    }
}
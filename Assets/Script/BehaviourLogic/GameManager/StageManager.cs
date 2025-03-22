using UnityEngine;

public class StageManager : MonoBehaviour
{
    public GameObject[] stages; 
    private int currentStage = 0;
    public PlayerInventory playerInventory; 
    public Movement playerMovement; 

    void Start()
    {
        UpdateStage();
    }

    public void NextStage()
    {
        if (CanMoveToNextStage()) 
        {
            stages[currentStage].SetActive(false);
            currentStage++; 
            if (currentStage < stages.Length)
            {
                stages[currentStage].SetActive(true); 
                ResetPlayerInventory(); 
                Debug.Log("Pindah ke Stage: " + (currentStage + 1));
            }
            else
            {
                Debug.Log("Semua stage sudah selesai!");
            }
        
        }
    }

    void UpdateStage()
    {
        for (int i = 0; i < stages.Length; i++)
        {
            stages[i].SetActive(i == currentStage);
        }
    }

    private bool CanMoveToNextStage()
    {
   
        if (currentStage == 0 && playerInventory.collectedKeys >= 3)
            return true;
        //if (currentStage == 1 && playerInventory.collectedNotes >= 5)
        //    return true;
        //if (currentStage == 2 && playerInventory.collectedSpider >= 7) // Gunakan jumlah kaki laba-laba
        //    return true;

        return false;
    }

    private void ResetPlayerInventory()
    {
        playerInventory.collectedKeys = 0;
        playerInventory.collectedPotions = 0;
        playerInventory.collectedRemotes = 0;
        Debug.Log("Semua item telah direset saat berpindah stage.");
    }
}

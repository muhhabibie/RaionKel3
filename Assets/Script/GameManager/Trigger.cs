using UnityEngine;

public class StageTrigger : MonoBehaviour
{
    public StageManager stageManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player memasuki gerbang, berpindah ke stage berikutnya.");
            stageManager.NextStage();
        }
    }
}

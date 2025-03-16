using UnityEngine;

public class Hiding : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EnemyAI enemy = FindFirstObjectByType<EnemyAI>();
            if (enemy != null)
            {
                enemy.SetPlayerHidden(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EnemyAI enemy = FindFirstObjectByType<EnemyAI>();
            if (enemy != null)
            {
                enemy.SetPlayerHidden(false);
            }
        }
    }

}

using UnityEngine;

public class SpiderWeb : MonoBehaviour
{
    public float slowSpeed = 2f;
    private float normalSpeed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Movement playerMovement = other.GetComponent<Movement>();
            if (playerMovement != null)
            {
                normalSpeed = playerMovement.GetSpeed();
                playerMovement.SetSpeed(slowSpeed);
                Debug.Log("Player terkena jaring laba-laba! Kecepatan dikurangi.");

                EnemyAI enemy = FindFirstObjectByType<EnemyAI>();
                if (enemy != null)
                {
                    enemy.ForceChasePlayer();
                    Debug.Log("Musuh langsung mengejar Player karena terkena jaring laba-laba!");
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Movement playerMovement = other.GetComponent<Movement>();
            if (playerMovement != null)
            {
                playerMovement.SetSpeed(normalSpeed);
                Debug.Log("Player keluar dari jaring laba-laba! Kecepatan kembali normal.");
            }
        }
    }
}

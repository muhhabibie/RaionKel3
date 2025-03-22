using UnityEngine;

public class SpiderWeb : MonoBehaviour
{
    float slowSpeed = 10f;
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
                playerMovement.ResetSpeedToOriginal();
                Debug.Log("Player keluar dari jaring laba-laba! Kecepatan kembali normal.");
            }
        }
    }
}

using UnityEngine;

public class RemoteItem : ItemBase
{
    public float stunDuration = 5f; 

    public override void Use()
    {
        if (quantity > 0) 
        {
            quantity--;
            EnemyAI enemy = FindFirstObjectByType<EnemyAI>(); 
            if (enemy != null)
            {
                enemy.StunEnemy(stunDuration);
                Debug.Log("Remote digunakan! Musuh terkena stun.");
            }
        }
        else
        {
            Debug.Log("Tidak ada Remote yang bisa digunakan!");
        }
    }
}

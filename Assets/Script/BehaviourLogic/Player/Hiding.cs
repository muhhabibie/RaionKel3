using UnityEngine;

public class Hiding : MonoBehaviour
{
    private SpriteRenderer playerSpriteRenderer;
    private Color originalColor;

    void Start()
    {
        // Mendapatkan komponen SpriteRenderer dari objek pemain
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = playerSpriteRenderer.color; // Menyimpan warna asli pemain
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("bush"))
        {
            // Mengubah warna pemain menjadi lebih transparan
            Color transparentColor = new Color(originalColor.r, originalColor.g, originalColor.b, 0.2f); // Menurunkan alpha untuk transparansi
            playerSpriteRenderer.color = transparentColor;

            // Beritahu musuh bahwa pemain sedang tersembunyi
            EnemyAI enemy = FindFirstObjectByType<EnemyAI>();
            if (enemy != null)
            {
                enemy.SetPlayerHidden(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("bush"))
        {
            // Kembalikan warna pemain menjadi tidak transparan
            playerSpriteRenderer.color = originalColor;

            // Beritahu musuh bahwa pemain tidak tersembunyi lagi
            EnemyAI enemy = FindFirstObjectByType<EnemyAI>();
            if (enemy != null)
            {
                enemy.SetPlayerHidden(false);
            }
        }
    }
}

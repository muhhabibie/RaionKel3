using UnityEngine;

public class Hiding : MonoBehaviour
{
    private SpriteRenderer playerSpriteRenderer;
    private Color originalColor;

    public AudioClip enterBushSound; // Suara saat masuk semak
    public AudioClip exitBushSound;  // Suara saat keluar dari semak

    private AudioSource audioSource;

    void Start()
    {
        // Mendapatkan komponen SpriteRenderer dari objek pemain
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = playerSpriteRenderer.color; // Menyimpan warna asli pemain

        // Menambahkan AudioSource jika belum ada
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("bush"))
        {
            // Mengubah warna pemain menjadi lebih transparan
            Color transparentColor = new Color(originalColor.r, originalColor.g, originalColor.b, 0.2f);
            playerSpriteRenderer.color = transparentColor;

            // Mainkan efek suara masuk semak (dibatasi 2 detik)
            if (enterBushSound != null)
            {
                audioSource.PlayOneShot(enterBushSound);
                Invoke("StopSound", 2f); // Hentikan suara setelah 2 detik
            }

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

            // Mainkan efek suara keluar semak (dibatasi 2 detik)
            if (exitBushSound != null)
            {
                audioSource.PlayOneShot(exitBushSound);
                Invoke("StopSound", 2f); // Hentikan suara setelah 2 detik
            }

            // Beritahu musuh bahwa pemain tidak tersembunyi lagi
            EnemyAI enemy = FindFirstObjectByType<EnemyAI>();
            if (enemy != null)
            {
                enemy.SetPlayerHidden(false);
            }
        }
    }

    private void StopSound()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}

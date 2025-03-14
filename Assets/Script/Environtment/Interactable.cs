using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    // Metode untuk menginteraksikan objek (dibuka, diambil, dll.)
    public abstract void Interact();

    // Metode untuk memperbarui UI yang terkait dengan objek
    public abstract void UpdateUI();
}







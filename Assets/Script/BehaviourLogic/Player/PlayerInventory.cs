using UnityEngine;
using static UnityEditor.Progress;

public class PlayerInventory : MonoBehaviour
{
    public int collectedKeys = 0;
    public int collectedPotions = 0;
    public int collectedRemotes = 0;

    public Movement playerMovement; // Referensi ke Movement script


    private void Start()
    {
        playerMovement = GetComponent<Movement>();
    }

    // Fungsi untuk menambah item ke inventaris
    public void AddItem(Item item)
    {
        if (item.itemName == "Key")
        {
            collectedKeys += item.quantity;
        }
        else if (item.itemName == "Potion")
        {
            collectedPotions += item.quantity;
        }
        else if (item.itemName == "Remote")
        {
            collectedRemotes += item.quantity;
        }

        Debug.Log("Item ditambah! " + item.itemName + " x" + item.quantity);
    }


}

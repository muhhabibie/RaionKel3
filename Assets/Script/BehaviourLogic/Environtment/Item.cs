using UnityEngine;

[System.Serializable] // Agar bisa ditampilkan di Inspector
public class Item
{
    public string itemName;  // Nama item (contoh: "Potion", "Remote", "Key")
    public ItemType itemType; // Jenis item
    public int quantity;  // Jumlah item

    public Item(string name, ItemType type, int qty)
    {
        itemName = name;
        itemType = type;
        quantity = qty;
    }
}

// Enum untuk tipe item
public enum ItemType
{
    Key,      // Untuk membuka gerbang ke stage berikutnya
    Potion,   // Untuk meningkatkan movement speed
    Remote    // Untuk men-stun laba-laba
}

using UnityEngine;

public abstract class ItemBase : MonoBehaviour
{
    public string itemName;
    public int quantity;

    public abstract void Use();


}

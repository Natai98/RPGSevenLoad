using UnityEngine;

public enum ItemType
{
    Equipment,
    Consumable,
    Etc
}

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject
{
    [SerializeField] protected int item_Id;
    public int itemId { get { return item_Id; } }

    [SerializeField] protected string item_Name;
    public string itemName { get { return item_Name; } }

    [SerializeField] protected Sprite item_Image;
    public Sprite itemImage { get { return item_Image; } }

    [SerializeField] protected int max_Count;
    public int maxCount { get { return max_Count; } }

    [SerializeField] protected ItemType item_Type;
    public ItemType itemType { get { return item_Type; } }

    [SerializeField] protected int item_Effect;
    public int itemEffect { get { return item_Effect; } }

    [TextArea(3, 5)]
    public string itemDescription = "This is a item.";


    public virtual void Use()
    {
        if (itemType == ItemType.Consumable)
        {
            GameManager.Instance.player.TakeDamage(-itemEffect);
        }
    }


    
}

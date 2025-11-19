using UnityEngine;

public class ItemControl : MonoBehaviour, IGetObject
{
    [SerializeField] private Item item;
    [SerializeField] private int amount;

    public void GetItem()
    {
        if (item != null)
        {
            bool pickUp = InventoryManager.Instance.AddItem(item, amount);
            if (pickUp)
            {
                Destroy(this.gameObject);
            }
        }
    }
}

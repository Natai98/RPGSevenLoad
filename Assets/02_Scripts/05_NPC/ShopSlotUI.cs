using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlotUI : MonoBehaviour
{
    [SerializeField] private Item goods;
    [SerializeField] private Image goodImg;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private int cost;

    private void Start()
    {
        if (goods != null)
        {
            costText.text = "" + cost;
            goodImg.sprite = goods.itemImage;
        }
        else
        {
            goodImg.enabled = false;
        }
    }

    public void Purchase()
    {
        if (GameManager.Instance.money >= cost && goods != null)
        {
            InventoryManager.Instance.AddItem(goods, 1);
            GameManager.Instance.PayMoney(cost);
        }
    }
}

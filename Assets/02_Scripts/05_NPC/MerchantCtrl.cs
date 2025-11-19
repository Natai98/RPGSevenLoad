using UnityEngine;
using UnityEngine.UI;

public class MerchantCtrl : MonoBehaviour
{
    [SerializeField] private Image TextPannal;
    [SerializeField] private Image ShopPannal;

    public void OpenShop()
    {
        GameManager.Instance.isShopping = true;
        GameManager.Instance.player.GetComponent<InventoryView>()?.OpenInventory();
        TextPannal.gameObject.SetActive(false);
        ShopPannal.gameObject.SetActive(true);
    }

    public void CloseShop()
    {
        GameManager.Instance.isShopping = false;
        GameManager.Instance.player.GetComponent<InventoryView>()?.CloseInventory();
        TextPannal.gameObject.SetActive(true);
        ShopPannal.gameObject.SetActive(false);
    }
}

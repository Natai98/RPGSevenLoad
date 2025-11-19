using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    // 각 슬롯 오브젝트에 적용

    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemCountText;
    private Button slotButton;
    private int slotIndex;

    private void Awake()
    {
        slotButton = GetComponent<Button>();
    }
    private void Start()
    {
        slotButton.onClick.AddListener(OnSlotClick);
    }

    public void SlotInit(int index)
    {
        slotIndex = index;
        UpdateSlotUI();
    }

    public void UpdateSlotUI()
    {
        InventorySlot slot = InventoryManager.Instance.GetSlot(slotIndex);

        if (slot != null && slot.item != null)
        {
            itemIcon.sprite = slot.item.itemImage;
            itemIcon.enabled = true;

            if (slot.currentCount > 1)
            {
                itemCountText.text = slot.currentCount.ToString();
                itemCountText.enabled = true;
            }
            else
            {
                itemCountText.enabled = false;
            }
        }
        else
        {
            itemIcon.enabled = false;
            itemCountText.enabled = false;
        }

    }

    private void OnSlotClick()
    {
        InventoryManager.Instance.UseItem(slotIndex);
    }

    private void OnDestroy()
    {
        if (slotButton != null)
        {
            slotButton.onClick.RemoveListener(OnSlotClick);
        }
    }

}

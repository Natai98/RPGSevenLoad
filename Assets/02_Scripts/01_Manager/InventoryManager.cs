using System;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    [SerializeField] private InventorySlot[] inventorySlots = new InventorySlot[12];

    public Action OnInventoryChanged;

    private void Start()
    {
        InitializeInventory();
    }

    private void InitializeInventory()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            inventorySlots[i] = new InventorySlot(null, 0);
        }
    }

    public bool AddItem(Item itemToAdd, int count = 1)
    {
        if (itemToAdd == null || count <= 0) return false;

        bool added = false;

        // 이미 인벤토리에 같은 아이템이 있는지 확인하여 스택 가능하면 갯수 증가
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i].item == itemToAdd && itemToAdd.maxCount > 1 && inventorySlots[i].currentCount < itemToAdd.maxCount)
            {
                // 같은 아이템이며
                // 스택 가능한 아이템이며
                // 아직 스택 여유가 있을 경우

                int remainingSpcae = itemToAdd.maxCount - inventorySlots[i].currentCount;
                int amountToAdd = Mathf.Min(count, remainingSpcae);

                inventorySlots[i].currentCount += amountToAdd;
                count -= amountToAdd;
                added = true;

                if (count <= 0) break;
            }
        }

        // 남은 아이템이 있다면 비어있는 슬롯에 추가
        if (count > 0)
        {
            for (int i = 0; i < inventorySlots.Length; i++)
            {
                if (inventorySlots[i].item == null)
                {
                    inventorySlots[i] = new InventorySlot(itemToAdd, count);
                    added = true;
                    count = 0;
                    break;
                }
            }
        }

        if (added)
        {
            Debug.Log("아이템 " + itemToAdd.itemName + " " + count + "개 획득");
            OnInventoryChanged?.Invoke();
        }
        else
        {
            Debug.Log("인벤토리가 가득찼습니다.");
        }

        return added;
    }

    // 인벤토리 슬롯에 있는 아이템 사용 (클릭 시 호출)
    public void UseItem(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= inventorySlots.Length) return;

        InventorySlot slot = inventorySlots[slotIndex];

        if (slot.item != null && slot.currentCount > 0)
        {
            if (GameManager.Instance.isCooking)
            {
                GameObject.FindGameObjectWithTag("CookSpot")?.GetComponent<CookSpot>().CookingFood(slot.item);
            }
            else
            {
                slot.item.Use();
            }
            slot.currentCount--;

            if (slot.currentCount <= 0)
            {
                slot.ClearSlot();
            }
            OnInventoryChanged?.Invoke();
        }
    }

    // 특정 슬롯의 아이템 정보 반환 (UI 에서 참조할 때 사용)
    public InventorySlot GetSlot(int index)
    {
        if (index >= 0 && index < inventorySlots.Length)
        {
            return inventorySlots[index];
        }
        return null;
    }

    // 모든 슬롯에 대한 정보 반환 (InventoryView 에서 UI 초기화 시 사용)
    public InventorySlot[] GetAllSlots()
    {
        return inventorySlots;
    }
}

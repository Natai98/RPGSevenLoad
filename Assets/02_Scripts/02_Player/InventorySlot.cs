using UnityEngine;

[System.Serializable]
public class InventorySlot      // InventoryManager 에서 사용될 예정
{
    public Item item;   // 이 슬롯에 저장된 아이템의 데이터
    public int currentCount;    // 이 슬롯에 저장된 아이템의 현재 갯수

    public InventorySlot(Item _item, int _count)
    {
        item = _item;
        currentCount = _count;
    }

    public void ClearSlot()
    {
        item = null;
        currentCount = 0;
    }
}

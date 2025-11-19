using System.Collections.Generic;
using UnityEngine;

public class InventoryView : MonoBehaviour
{
    // PlayerUI 에 적용

    [SerializeField] private InventorySlotUI[] slotUIs;
    [SerializeField] private GameObject invevtoryPannal;

    private bool isOpened = false;

    private void Start()
    {
        invevtoryPannal.SetActive(isOpened);
        InitializeInventoryUI();
    }

    public void OpenInventory()
    {
        isOpened = true;
        invevtoryPannal.SetActive(isOpened);
    }

    public void CloseInventory()
    {
        isOpened = false;
        invevtoryPannal.SetActive(isOpened);
    }

    private void OnEnable()
    {
        InventoryManager.Instance.OnInventoryChanged += RefreshAllSlotUI;
    }

    private void OnDisable()
    {
        InventoryManager.Instance.OnInventoryChanged -= RefreshAllSlotUI;
    }

    private void InitializeInventoryUI()
    {
        InventorySlot[] allSlots = InventoryManager.Instance.GetAllSlots();
        for (int i = 0; i < allSlots.Length; i++)
        {
            if (slotUIs[i] != null)
            {
                slotUIs[i].SlotInit(i);
            }
        }
        RefreshAllSlotUI();
    }

    private void RefreshAllSlotUI()
    {
        foreach (InventorySlotUI slotUI in slotUIs)
        {
            slotUI.UpdateSlotUI();
        }
    }

    private void Update()
    {
        if (GameManager.Instance.isCooking || GameManager.Instance.isShopping) return;

        if (Input.GetKeyDown(KeyCode.R))
        {
            isOpened = !isOpened;
            invevtoryPannal.SetActive(isOpened);
        }
    }
}

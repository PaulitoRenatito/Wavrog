using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private RectTransform inventoryPanel;
    [SerializeField] private Inventory inventory;

    private ObjectPool itemPool;

    private void Start()
    {
        itemPool = GetComponent<ObjectPool>();
        GameInput.Instance.OnOpenInventoryAction += GameInput_OnOpenInventoryAction;
        inventoryPanel.gameObject.SetActive(false);
    }

    private void GameInput_OnOpenInventoryAction(object sender, EventArgs e)
    {
        if (inventoryPanel.gameObject.activeSelf)
        {
            GameInput.SetCursorMode(CursorLockMode.Locked);
            itemPool.ReturnActivePoolObjects();
            inventoryPanel.gameObject.SetActive(false);
        }
        else
        {
            GameInput.SetCursorMode(CursorLockMode.None);
            inventoryPanel.gameObject.SetActive(true);
            List<ItemSO> items = inventory.GetItems();
            foreach (ItemSO item in items)
            {
                ItemUI itemContainer = itemPool.GetPoolObject().GetComponent<ItemUI>();
                itemContainer.SetItemName(item.objectName);
            }
        }
    }
}

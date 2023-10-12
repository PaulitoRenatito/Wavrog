using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasPlayer : MonoBehaviour
{

    void Start()
    {
        GameInput.Instance.OnOpenInventoryAction += GameInput_OnOpenInventoryAction;
    }

    private void GameInput_OnOpenInventoryAction(object sender, EventArgs e)
    {
        // if (inventoryPanel.gameObject.activeSelf)
        // {
        //     GameInput.SetCursorMode(CursorLockMode.Locked);
        // }
        // else
        // {
        //     inventoryPanel.gameObject.SetActive(true);
        //     GameInput.SetCursorMode(CursorLockMode.None);
        // }
    }
}

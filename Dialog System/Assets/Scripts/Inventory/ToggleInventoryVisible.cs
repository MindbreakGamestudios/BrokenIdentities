using Assets.Scripts.Inventory;
using Assets.Scripts.Scenes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleInventoryVisible : MonoBehaviour
{
    public InventoryUI InventoryRoot;
    public Inventory PlayerInventory;

    void Update()
    {
        if (Input.GetButtonDown("ShowHideInventory"))
        {
            if (InventoryRoot.gameObject.activeSelf)
            {
                InventoryRoot.gameObject.SetActive(false);
                CursorLocker.LockCursor();
            }
            else
            {
                InventoryRoot.ShowInventory(PlayerInventory);
                CursorLocker.UnlockCursor();
            }
        }
    }
}

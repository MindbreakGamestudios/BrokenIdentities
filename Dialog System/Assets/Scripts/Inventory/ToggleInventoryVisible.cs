using Assets.Scripts.Inventory;
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
                InventoryRoot.gameObject.SetActive(false);
            else
                InventoryRoot.ShowInventory(PlayerInventory);
        }
    }
}

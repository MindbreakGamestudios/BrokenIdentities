using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleInventoryVisible : MonoBehaviour
{
    public  GameObject InventoryRoot;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            InventoryRoot.SetActive(!InventoryRoot.activeSelf);
        }
    }
}

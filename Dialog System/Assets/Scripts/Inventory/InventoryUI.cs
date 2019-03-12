using Assets.Scripts.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Inventory
{
    public class InventoryUI : MonoBehaviour
    {
        public Inventory DisplayedInventory;
        public Transform ContentPanel;

        public Image ItemIcon;
        public Text ItemName;

        void Start()
        {
            if (DisplayedInventory == null || ContentPanel == null || SlotPrefab == null)
            {
                Debug.LogError("Not all necessary references are set.");
                return;
            }

            DisplayedInventory.ItemAdded += CreateItemButton;

            foreach (var item in DisplayedInventory.Items)
            {
                CreateItemButton(item);
            }
        }

        public GameObject SlotPrefab;
        void CreateItemButton(Item item)
        {
            var slot = Instantiate(SlotPrefab);
            slot.transform.SetParent(ContentPanel);

            var behav = slot.GetComponent<InventorySlot>();
            behav.Image.sprite = item.Icon;
            behav.Button.onClick.AddListener(() => DisplayItem(item));
        }

        private void DisplayItem(Item item)
        {
            ItemIcon.sprite = item?.Icon;
            ItemName.text = item?.Name;
        }
    }
}

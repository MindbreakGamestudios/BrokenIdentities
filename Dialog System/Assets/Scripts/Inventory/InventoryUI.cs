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

            foreach (var item in DisplayedInventory.Items)
            {
                CreateItemButton(item);
            }
        }

        public GameObject SlotPrefab;
        void CreateItemButton(Item item)
        {
            var btn = Instantiate(SlotPrefab);
            btn.GetComponent<Image>().sprite = item.Icon;
            btn.transform.SetParent(ContentPanel);

            var comp = btn.GetComponent<Button>();
            comp.onClick.AddListener(() => selectedItem = item);
        }


        private Item selectedItem;
        private Item lastSelectedItem;
        void Update()
        {
            if (!Equals(selectedItem, lastSelectedItem))
            {
                ItemIcon.sprite = selectedItem?.Icon;
                ItemName.text = selectedItem?.Name;
            }
        }

    }
}

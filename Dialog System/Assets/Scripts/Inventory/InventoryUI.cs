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
        public Transform ContentPanel;

        public Text ItemDescription;
        public Image ItemIcon;
        public Text ItemName;

        private void Start()
        {
            if (ItemDescription == null)
                Debug.LogError("ItemDescription property is not set.");

            if (ItemIcon == null)
                Debug.LogError("ItemIcon property is not set.");

            if (ItemName == null)
                Debug.LogError("ItemName property is not set.");

            ItemDescription.text = String.Empty;
            ItemIcon.sprite = null;
            ItemName.text = string.Empty;

            gameObject.SetActive(false);
            CreateNecessarySlots(20);
        }

        private List<GameObject> createdSlots = new List<GameObject>(50);
        public void ShowInventory(Inventory inventory)
        {
            if (inventory == null) return;

            gameObject.SetActive(true);

            DeactivateUnusedSlot(inventory);
            CreateNecessarySlots(inventory.Items.Count);

            for (int i = 0; i < inventory.Items.Count; i++)
            {
                var slot = createdSlots[i];
                slot.SetActive(true);

                var item = inventory.Items[i];
                var behav = slot.GetComponent<InventorySlot>();
                behav.Image.sprite = item.Icon;
                behav.Button.onClick.RemoveAllListeners();
                behav.Button.onClick.AddListener(() => DisplayItem(item));
            }
        }

        private void DeactivateUnusedSlot(Inventory inventory)
        {
            if (createdSlots.Count <= inventory.Items.Count) { return; }

            for (int i = inventory.Items.Count; i < createdSlots.Count; i++)
            {
                createdSlots[i].SetActive(false);
            }
        }
        
        public GameObject SlotPrefab;
        private void CreateNecessarySlots(int itemsCount)
        {
            if (itemsCount <= createdSlots.Count) { return; }

            for (int i = createdSlots.Count; i < itemsCount; i++)
            {
                var slot = Instantiate<GameObject>(SlotPrefab);
                slot.transform.SetParent(ContentPanel);

                createdSlots.Add(slot);
            }
        }
        

        private void DisplayItem(Item item)
        {
            if (item == null)
            {
                ItemDescription.text = String.Empty;
                ItemIcon.sprite = null;
                ItemName.text = String.Empty;
            }
            else
            {
                ItemDescription.text = item.Description;
                ItemIcon.sprite = item.Icon;
                ItemName.text = item.Name;
            }           

            if (ItemIcon.sprite != null)
                ItemIcon.color = Color.white;
            else
                ItemIcon.color = new Color(0, 0, 0, 0);
        }
    }
}

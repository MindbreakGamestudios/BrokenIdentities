using Assets.Scripts.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Items.UI
{
    public class ItemFoundUI : MonoBehaviour
    {
        public Button cmdGotIt;
        
        public Image picItemIcon;

        public Text txtItemDescription;
        public Text txtItemName;

        void Start()
        {
            if (cmdGotIt == null)
                Debug.LogError("cmdGotIt is null");

            if (picItemIcon == null)
                Debug.LogError("picItemIcon is null");

            if (txtItemDescription == null)
                Debug.LogError("txtItemDescription is null");

            if (txtItemName == null)
                Debug.LogError("txtItemName is null");

            ItemEvents.ItemFound += ShowItemFound;
            cmdGotIt.onClick.AddListener(new UnityEngine.Events.UnityAction(Close));

            gameObject.SetActive(false);    //Set as Invisible after start.
        }

        void ShowItemFound(Item item)
        {
            if (item == null) return;

            picItemIcon.sprite = item.Icon;
            txtItemDescription.text = item.Description;
            txtItemName.text = item.Name;

            Show();
        }

        private void Show()
        {
            gameObject.SetActive(true);
            CursorLocker.UnlockCursor();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
            {
                Close();
            }
        }

        private void Close()
        {
            gameObject.SetActive(false);
            CursorLocker.LockCursor();
        }
    }
}

using Assets.Scripts.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Inventory
{
    public class Inventory : MonoBehaviour
    {
        public List<Item> Items = new List<Item>();

        public delegate void ItemAddedEventHandler(Item item);
        public event ItemAddedEventHandler ItemAdded;
        public void AddItem(Item item)
        {
            Items.Add(item);
            ItemAdded?.Invoke(item);
        }

        public delegate void ItemRemovedEventHandler(Item item);
        public event ItemRemovedEventHandler ItemRemoved;
        public void RemoveItem(Item item)
        {
            Items.Remove(item);
            ItemRemoved?.Invoke(item);
        }
    }
}

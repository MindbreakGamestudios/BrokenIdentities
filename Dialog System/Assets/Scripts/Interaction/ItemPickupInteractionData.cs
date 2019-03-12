using Assets.Scripts.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Interaction
{
    public class ItemPickupInteractionData : MonoBehaviour
    {
        [Tooltip("The item to be sent to the inventory")]
        public Item Item;
    }
}

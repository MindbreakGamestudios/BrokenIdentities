using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Items
{
    [CreateAssetMenu(fileName = "Item", menuName = "Custom/Item", order = 1)]
    public class Item : ScriptableObject
    {
        public Sprite Icon;
        public string Name;
    }
}

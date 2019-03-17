using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Items
{
    public static class ItemEvents
    {
        public delegate void ItemFoundHandler(Item item);
        public static event ItemFoundHandler ItemFound;
        public static void OnItemFound(Item item)
        {
            ItemFound?.Invoke(item);
        }
    }
}

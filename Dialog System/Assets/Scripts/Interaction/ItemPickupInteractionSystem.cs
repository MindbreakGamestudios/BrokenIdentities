using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Entities;

namespace Assets.Scripts.Interaction
{
    [UpdateAfter(typeof(ClickInteractionSystem))]
    public class ItemPickupInteractionSystem : ComponentSystem
    {
        struct Data
        {
            public readonly int Length;
            public ComponentArray<ClickInteractionComponent> ClickComponents;
            public ComponentArray<ItemPickupInteractionData> ItemComponents;
        }

        [Inject] private Data data;

        protected override void OnUpdate()
        {
            var inventory = Player.PlayerData.PlayerInformation?.Inventory;
            if (data.Length == 0 || inventory == null) { return; }

            for (int i = 0; i < data.Length; i++)
            {
                var click = data.ClickComponents[i];
                var item = data.ItemComponents[i];

                if (click.LeftClick && item.Item != null && !item.HasBeenPickedUp)
                {
                    inventory.AddItem(item.Item);
                    item.HasBeenPickedUp = true;
                }
            }
        }
    }
}

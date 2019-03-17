using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Entities;

namespace Assets.Scripts.Interaction.Doors
{
    [UpdateAfter(typeof(ClickInteractionSystem))]
    public class OpenDoorSystem : ComponentSystem
    {
        struct Data
        {
            public readonly int Length;
            public ComponentArray<DoorData> Doors;
            public ComponentArray<ClickInteractionComponent> Clicks;
        }

        [Inject] private Data data;

        protected override void OnUpdate()
        {
            var inventory = Player.PlayerData.PlayerInformation.Inventory;
            if (inventory == null) { return; }

            for (int i = 0; i < data.Length; i++)
            {
                var click = data.Clicks[i];
                var door = data.Doors[i];

                if (!door.IsOpen && click.LeftClick)
                {
                    OpenDoorIfPossible(inventory, door);
                }
            }
        }

        private static void OpenDoorIfPossible(Inventory.Inventory inventory, DoorData door)
        {
            foreach (var item in inventory.Items)
            {
                if (Equals(item, door.NeededKey))
                {
                    door.IsOpen = true;
                }
            }
        }
    }
}

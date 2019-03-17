using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Entities;

namespace Assets.Scripts.Interaction.Doors
{
    [UpdateAfter(typeof(OpenDoorSystem))]
    class TeleportPlayerSystem : ComponentSystem
    {
        struct Data
        {
            public readonly int Length;
            public ComponentArray<DoorData> Doors;
            public ComponentArray<TeleportPlayerData> Teleports;
            public ComponentArray<ClickInteractionComponent> Clicks;
        }

        [Inject] private Data data;

        protected override void OnUpdate()
        {
            for (int i = 0; i < data.Length; i++)
            {
                var click = data.Clicks[i];
                var door = data.Doors[i];
                var teleport = data.Teleports[i];

                if(click.LeftClick && door.IsOpen && teleport.JumpPoint != null)
                {
                    Player.PlayerEvents.OnTeleportPlayer(teleport.JumpPoint);
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Entities;

namespace Assets.Scripts.Interaction
{
    class DeactivateObjectAfterPickupSystem : ComponentSystem
    {
        struct Data
        {
            public readonly int Length;
            public ComponentArray<DeactivateObjectAfterPickup> Deactivates;
            public ComponentArray<ItemPickupInteractionData> Pickups;
        }

        [Inject] private Data data;

        protected override void OnUpdate()
        {
            for (int i = 0; i < data.Length; i++)
            {
                var pickup = data.Pickups[i];
                if (pickup.HasBeenPickedUp)
                {
                    pickup.gameObject.SetActive(false);
                }
            }
        }
    }
}

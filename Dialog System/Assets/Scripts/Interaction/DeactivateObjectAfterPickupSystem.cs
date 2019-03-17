using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Entities;
using UnityEngine;

namespace Assets.Scripts.Interaction
{
    [UpdateAfter(typeof(ItemPickupInteractionSystem))]
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
            var toDeactivate = new List<GameObject>();

            for (int i = 0; i < data.Length; i++)
            {
                var pickup = data.Pickups[i];
                if (pickup.HasBeenPickedUp && pickup.gameObject.activeSelf)
                {
                    toDeactivate.Add(pickup.gameObject);
                }
            }

            foreach (var obj in toDeactivate)
                obj.SetActive(false);
        }
    }
}

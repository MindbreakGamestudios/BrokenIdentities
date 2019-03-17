using Assets.Scripts.HoveEffects;
using Assets.Scripts.Interaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Entities;
using UnityEngine;

namespace Assets.Scripts.HoveEffects
{
    class DisableHoverAnimationAfterItemPickupSystem : ComponentSystem
    {
        struct Data
        {
            public readonly int Length;
            public ComponentArray<ItemPickupInteractionData> Pickups;
            public ComponentArray<DisableHoverAnimationAfterItemPickup> Disables;
            public ComponentArray<HoverAnimationData> Hovers;
        }

        [Inject] private Data data;

        protected override void OnUpdate()
        {
            for (int i = 0; i < data.Length; i++)
            {
                if(data.Pickups[i].HasBeenPickedUp)
                {
                    data.Hovers[i].Enabled = false;
                }
            }
        }

    }
}

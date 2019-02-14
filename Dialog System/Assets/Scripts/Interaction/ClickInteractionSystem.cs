using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Unity.Entities;

namespace Assets.Scripts.Interaction
{
    class ClickInteractionSystem : ComponentSystem
    {

        struct InteractionEntity
        {
            public readonly int Length;
            public ComponentArray<ColliderComponent> colliders;
            public ComponentArray<ClickInteractionComponent> clickInteractions;
        }

        [Inject] private InteractionEntity entity;
        protected override void OnUpdate()
        {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var leftClick = Input.GetMouseButtonDown(0);
            var rightClick = Input.GetMouseButtonDown(1);

            for (int i = 0; i < entity.Length; i++)
            {
                ColliderComponent collider = entity.colliders[i];
                ClickInteractionComponent clickInteraction = entity.clickInteractions[i];

                bool collides = collider.Collider.OverlapPoint(mousePos);

                clickInteraction.LeftClick = collides && leftClick;
                clickInteraction.RightClick = collides && rightClick;
            }

        }

    }
}

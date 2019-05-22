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
            if (IsMouseOverGUIElement())
                return;

            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var leftClick = Input.GetMouseButtonDown(0);
            var rightClick = Input.GetMouseButtonDown(1);

            var ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward.normalized);
            var hit = Physics.Raycast(ray, out var hitInfo, 10, ~(1 << 9));  //Don't interact with player
            
            for (int i = 0; i < entity.Length; i++)
            {
                ColliderComponent collider = entity.colliders[i];
                ClickInteractionComponent clickInteraction = entity.clickInteractions[i];

                bool collides = hit && hitInfo.collider != null && Equals(hitInfo.collider, collider.Collider);

                clickInteraction.WasHovered = clickInteraction.IsHovered;
                clickInteraction.IsHovered = collides;
                clickInteraction.LeftClick = collides && leftClick;
                clickInteraction.RightClick = collides && rightClick;
            }

        }

        private bool IsMouseOverGUIElement()
        {
            var eventSystem = UnityEngine.EventSystems.EventSystem.current;
            if (eventSystem == null)
                return false;

            var pointerData = new UnityEngine.EventSystems.PointerEventData(eventSystem) { position = Input.mousePosition };
            var hits = new List<UnityEngine.EventSystems.RaycastResult>();
            eventSystem.RaycastAll(pointerData, hits);

            return (hits.Count != 0);
        }

    }
}

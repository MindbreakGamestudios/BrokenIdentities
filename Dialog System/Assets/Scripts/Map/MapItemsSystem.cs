using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Entities;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Map
{
    class MapItemsSystem : ComponentSystem
    {

#pragma warning disable CS0649    
        struct Data
        {
            public readonly int Length;
            public ComponentArray<MapItem> Items;
            public ComponentArray<RectTransform> Transforms;
            public ComponentArray<Image> Images;
        }
#pragma warning restore CS0649


        [Inject] private Data data;

        protected override void OnUpdate()
        {
            if (data.Length == 0) return;

            var mouse = Input.mousePosition;
            mouse.z = -Camera.main.gameObject.transform.position.z;

            var mouseInWorld = Camera.main.ScreenToWorldPoint(mouse);
            for (int i = 0; i < data.Length; i++)
            {
                var transform = data.Transforms[i];
                var item = data.Items[i];
                var image = data.Images[i];
                
                SetSelectionState(mouse, image, transform, item);
            }
        }

        private static void SetSelectionState(Vector3 mouseInWorld, Image image, RectTransform transform, MapItem item)
        {
            var isSelected = Contains(mouseInWorld, image, transform);
            if (isSelected == item.isSelected) return;

            if (!item.isSelected )
                transform.localScale += new Vector3(0.2f, 0.2f, 0);
            else
                transform.localScale -= new Vector3(0.2f, 0.2f, 0);

            item.isSelected = !item.isSelected;
        }

        private static bool Contains(Vector3 mouse, Image image, RectTransform transform)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(image.canvas.transform as RectTransform, mouse, image.canvas.worldCamera, out var mousePos);

            var x = transform.anchoredPosition.x + transform.rect.x;
            var xWidth = x + transform.rect.width;

            var y = transform.anchoredPosition.y + transform.rect.y;
            var yHeight = y + transform.rect.height;

            var xOk = mousePos.x >= x && mousePos.x <= xWidth;
            var yOk = mousePos.y >= y && mousePos.y <= yHeight;

            return xOk && yOk;
        }
    }
}

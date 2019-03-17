using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Entities;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Map
{
    [UpdateAfter(typeof(MapItemsSystem))]
    class MapItemInteractionSystem : ComponentSystem
    {

#pragma warning disable CS0649
        struct Data
        {
            public readonly int Length;
            public ComponentArray<MapItem> Items;
        }
#pragma warning restore CS06449

        [Inject] private Data data;

        protected override void OnUpdate()
        {
            if (data.Length == 0 || !(Input.GetButton("Fire1"))) return;

            MapItem selectedItem = null;
            for (int i = 0; i < data.Length && selectedItem == null; i++)
            {
                var item = data.Items[i];
                if (item.isSelected)
                {
                    selectedItem = item;
                }
            }

            if (selectedItem == null)
                return;

            if (!string.IsNullOrWhiteSpace(selectedItem.SceneName))
                SceneManager.LoadScene(selectedItem.SceneName);
            else if (selectedItem.JumpPoint != null)
                Player.PlayerEvents.OnTeleportPlayer(selectedItem.JumpPoint);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public static class PlayerEvents
    {
        public delegate void TeleportPlayerToPositionHandler(Transform target);
        public static event TeleportPlayerToPositionHandler MovePlayer;
        public static void OnTeleportPlayer(Transform target)
        {
            if (target != null)
                MovePlayer?.Invoke(target);
        }

        public delegate void PlayerTeleportedHandler();
        public static event PlayerTeleportedHandler PlayerTeleported;
        public static void OnPlayerTeleported()
        {
            PlayerTeleported?.Invoke();
        }
    }
}

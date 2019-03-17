using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerData : MonoBehaviour
    {
        private static PlayerData playerInformation;
        public static PlayerData PlayerInformation => playerInformation;


        public float MoveSpeed = 2;
        public float SprintSpeed = 3;

        public Inventory.Inventory Inventory;

        void Start()
        {
            playerInformation = this;
            PlayerEvents.MovePlayer += TeleportPlayer;
        }

        void TeleportPlayer(Transform target)
        {
            gameObject.transform.position = target.position;
            Camera.main.transform.position = new Vector3(target.position.x, target.position.y, Camera.main.transform.position.z);

            PlayerEvents.OnPlayerTeleported();
        }
    }
}

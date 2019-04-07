using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Entities;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerInputSystem : ComponentSystem
    {
        private struct Entities
        {
            public readonly int Length;
            public ComponentArray<Rigidbody> Bodies;
            public ComponentArray<PlayerData> Players;
            public ComponentArray<Transform> Transforms;
        }

        [Inject] private Entities data;

        protected override void OnUpdate()
        {
            if (data.Length == 0) return;

            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");
            var dir = new Vector3(horizontal, 0f, vertical);
            
            var isSprinting = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
            for (int i = 0; i < data.Length; i++)
            {
                var body = data.Bodies[i];
                var player = data.Players[i];
                var transform = data.Transforms[i];

                var movementFactor = ((isSprinting) ? player.SprintSpeed : player.MoveSpeed);
                var movement = (transform.forward * vertical) + (transform.right * horizontal);

                body.velocity = movement.normalized * movementFactor;
            }
        }
    }
}

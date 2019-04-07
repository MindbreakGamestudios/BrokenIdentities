using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Entities;
using UnityEngine;

namespace Assets.Scripts.Player
{
    class PlayerMouseRotationSystem : ComponentSystem
    {
        struct Data
        {
            public readonly int Length;
            public ComponentArray<PlayerMouseRotationData> Movements;
            public ComponentArray<Transform> Transforms;
        }

        protected override void OnStartRunning()
        {
            base.OnStartRunning();
        }

        [Inject] private Data data;

        protected override void OnUpdate()
        {
            var deltaX = Input.GetAxisRaw("Mouse X");
                        
            for (int i = 0; i < data.Length; i++)
            {
                var transform = data.Transforms[i];
                var movement = data.Movements[i];

                if (!Mathf.Approximately(0, deltaX))
                {
                    var rotationAngle = deltaX * movement.MovementSpeed;
                    movement.DesiredRotation = (movement.DesiredRotation + rotationAngle) % 360;
                }

                var desiredRotQ = Quaternion.Euler(transform.eulerAngles.x, movement.DesiredRotation, transform.eulerAngles.z);
                transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotQ, Time.deltaTime * 10);
            }
        }
    }
}

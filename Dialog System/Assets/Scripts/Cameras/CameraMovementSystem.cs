using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Entities;
using UnityEngine;

namespace Assets.Scripts.Cameras
{
    class CameraMovementSystem : ComponentSystem
    {
        struct Data
        {
            public readonly int Length;
            public ComponentArray<CameraMovementData> Movements;
            public ComponentArray<Camera> Cameras;
        }

        protected override void OnStartRunning()
        {
            base.OnStartRunning();
        }

        [Inject] private Data data;

        protected override void OnUpdate()
        {
            var deltaY = Input.GetAxisRaw("Mouse Y");
            
            for (int i = 0; i < data.Length; i++)
            {
                var camera = data.Cameras[i];
                var movement = data.Movements[i];
                                
                if (!Mathf.Approximately(0, deltaY))
                {
                    var rotationAngle = (-1)*deltaY * movement.MovementSpeed;
                    movement.DesiredRotation += rotationAngle;
                }

                var desiredRotQ = Quaternion.Euler(movement.DesiredRotation, camera.transform.eulerAngles.y, camera.transform.eulerAngles.z);
                camera.transform.rotation = Quaternion.Lerp(camera.transform.rotation, desiredRotQ, Time.deltaTime * 5);
            }
        }
    }
}

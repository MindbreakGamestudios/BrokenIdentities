using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Cameras
{
    class CameraMovementData : MonoBehaviour
    {
        public float MovementSpeed = 1f;

        [HideInInspector]
        public float DesiredRotation = 0f;

        private void Start()
        {
            DesiredRotation = transform.rotation.eulerAngles.x;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player
{
    internal class PlayerMouseRotationData : MonoBehaviour
    {
        public float MovementSpeed = 1f;
        [HideInInspector]
        public float DesiredRotation = 0;
    }
}
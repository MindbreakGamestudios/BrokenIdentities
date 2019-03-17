using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Map
{
    class MapItem : MonoBehaviour
    {
        [HideInInspector]
        public bool isSelected;
        public Transform JumpPoint;

#pragma warning disable CS0649
        public string SceneName;
#pragma warning restore CS0649
    }
}

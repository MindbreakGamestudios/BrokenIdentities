using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Database
{
    [Serializable]
    public class LiteDatabase : ScriptableObject
    {
        public byte[] Data;
    }
}

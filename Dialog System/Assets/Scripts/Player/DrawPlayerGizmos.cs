using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player
{
    class DrawPlayerGizmos : MonoBehaviour
    {
        private void OnDrawGizmos()
        {
            var body = GetComponent<Rigidbody>();
            Gizmos.DrawLine(PlayerData.PlayerInformation.InteractionRaycastOrigin.position, Camera.main.transform.forward.normalized * 5);
        }
    }
}

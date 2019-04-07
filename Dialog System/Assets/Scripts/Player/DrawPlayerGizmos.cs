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
            Gizmos.DrawLine(transform.position, transform.forward * 200);
            Gizmos.DrawLine(transform.position, body.velocity * 200);

        }
    }
}

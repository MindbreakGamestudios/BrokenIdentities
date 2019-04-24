using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Scenes
{
    class CursorLocker
    {
        static int locked; 
        private static void SetLockState()
        {
            if (locked <= 0)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }

        public static void LockCursor()
        {
            locked += 1;
            SetLockState();
        }

        public static void UnlockCursor()
        {
            locked -= 1;
            SetLockState();
        }
    }
}

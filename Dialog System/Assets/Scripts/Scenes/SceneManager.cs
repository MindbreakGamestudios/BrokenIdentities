using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Scenes
{
    public class SceneManager : MonoBehaviour
    {
        void Start()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void LateUpdate()
        {
            if (Input.GetKey(KeyCode.Escape))
                Application.Quit();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Dialogsystem
{
    public class DialogConnection
    {
        public DialogConnectionPoint inPoint;
        public DialogConnectionPoint outPoint;
        public Action<DialogConnection> OnClickRemoveConnection;

        public DialogConnection(DialogConnectionPoint inPoint, DialogConnectionPoint outPoint,
            Action<DialogConnection> OnClickRemoveConnection)
        {
            this.inPoint = inPoint;
            this.outPoint = outPoint;
            this.OnClickRemoveConnection = OnClickRemoveConnection;
        }

        public void Draw()
        {
            Handles.DrawBezier(
                inPoint.rect.center,
                outPoint.rect.center,
                inPoint.rect.center + Vector2.left * 50f,
                outPoint.rect.center - Vector2.left * 50f,
                Color.white,
                null, 2f);

#pragma warning disable CS0618 // Typ oder Element ist veraltet
            if (Handles.Button((inPoint.rect.center + outPoint.rect.center) * 0.5f, Quaternion.identity, 4, 8, Handles.RectangleCap))
#pragma warning restore CS0618 // Typ oder Element ist veraltet
            {
                if(OnClickRemoveConnection != null)
                {
                    OnClickRemoveConnection(this);
                }
            }
        }

    }
}

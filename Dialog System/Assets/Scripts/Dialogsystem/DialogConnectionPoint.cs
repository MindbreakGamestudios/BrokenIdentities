using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Dialogsystem
{
    public enum DialogConnectionType { In, Out }

    public class DialogConnectionPoint
    {
        public Rect rect;
        public DialogConnectionType type;
        public DialogNode node;
        public GUIStyle style;

        public Action<DialogConnectionPoint> OnClickConnectionPoint;

        public DialogConnectionPoint(DialogNode node, DialogConnectionType type,
            GUIStyle style, Action<DialogConnectionPoint> clickAction)
        {
            this.node = node;
            this.type = type;
            this.style = style;
            this.OnClickConnectionPoint = clickAction;
            rect = new Rect(0, 0, 10f, 20f);
        }

        public void Draw()
        {
            rect.y = node.rect.y + (node.rect.height * 0.5f) - rect.height * 0.5f;
            switch (type)
            {
                case DialogConnectionType.In:
                    rect.x = node.rect.x - rect.width + 8f;
                    break;
                case DialogConnectionType.Out:
                    rect.x = node.rect.x + node.rect.width - 8f;
                    break;
            }

            if (GUI.Button(rect, "", style))
            {
                OnClickConnectionPoint?.Invoke(this);
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Dialogsystem
{
    public class DialogNode
    {
        public Rect rect;
        public bool isDragged;
        public bool isSelected;

        public GUIStyle style;
        public GUIStyle defaultNodeStyle;
        public GUIStyle selectedNodeStyle;

        public DialogConnectionPoint inPoint;
        public DialogConnectionPoint outPoint;

        public Action<DialogNode> OnRemoveDialogNode;
        public Action<DialogNode> OnSelectDialogNode;

        public DialogNode(Vector2 position, float width, float height,
            GUIStyle nodeStyle, GUIStyle selectedNodeStyle,
            GUIStyle inPointStyle, GUIStyle outPointStyle,
            Action<DialogConnectionPoint> OnClickInPoint,
            Action<DialogConnectionPoint> OnClickOutPoint,
            Action<DialogNode> OnRemoveDialogNode,
            Action<DialogNode> OnSelectDialogNode)
        {
            rect = new Rect(position.x, position.y, width, height);
            this.style = nodeStyle;
            this.defaultNodeStyle = nodeStyle;
            this.selectedNodeStyle = selectedNodeStyle;
            inPoint = new DialogConnectionPoint(this, DialogConnectionType.In, inPointStyle, OnClickInPoint);
            outPoint = new DialogConnectionPoint(this, DialogConnectionType.Out, outPointStyle, OnClickOutPoint);
            this.OnRemoveDialogNode = OnRemoveDialogNode;
            this.OnSelectDialogNode = OnSelectDialogNode;
        }

        public void Drag(Vector2 delta)
        {
                rect.position += delta;
        }

        public void Draw()
        {
            Color defaultColor = GUI.color;
            GUI.backgroundColor = Color.green;
            GUI.Box(rect, GUIContent.none, style);
            GUI.backgroundColor = defaultColor;
            DrawNodeContent();
            inPoint.Draw();
            outPoint.Draw();
        }

        public string DialogTitel = "New Dialog";
        private void DrawNodeContent()
        {
            Rect textRect = new Rect(
                rect.x + 20, rect.y + 12.5f,
                rect.width - 40, rect.height - 29);
            DialogTitel =  GUI.TextField(textRect, DialogTitel);
        }

        public bool ProcessEvents(Event e)
        {
            switch (e.type)
            {
                case EventType.MouseDown:
                    if(e.button == 0)
                    {
                        if (rect.Contains(e.mousePosition))
                        {
                            isDragged = true;
                            isSelected = true;
                            OnSelectDialogNode(this);
                            style = selectedNodeStyle;
                        }
                        else
                        {
                            isSelected = false;
                            //OnSelectDialogNode(null);
                            style = defaultNodeStyle;
                        }
                        GUI.changed = true;
                    }

                    if(e.button == 1 && isSelected && rect.Contains(e.mousePosition))
                    {
                        ProcessNodeContextMenu();
                        e.Use();
                    }

                    break;
                case EventType.MouseUp:
                    isDragged = false;
                    break;
                case EventType.MouseDrag:
                    if(e.button == 0 && isDragged)
                    {
                        Drag(e.delta);
                        e.Use();
                        return true;
                    }
                    break;
            }
            return false;
        }

        private void ProcessNodeContextMenu()
        {
            GenericMenu genericMenu = new GenericMenu();
            genericMenu.AddItem(new GUIContent("Remove Dialog-Entry"), false, OnClickRemoveNode);
            genericMenu.ShowAsContext();
        }

        private void OnClickRemoveNode()
        {
            if(OnRemoveDialogNode != null)
            {
                OnRemoveDialogNode(this);
            }
        }
    }
}

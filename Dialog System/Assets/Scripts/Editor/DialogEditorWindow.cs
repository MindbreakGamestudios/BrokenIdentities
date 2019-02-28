using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Assets.Scripts.Dialogsystem;
using System;
using System.Linq;
using Assets.Scripts.Dialogsystem.Models;
using Assets.Scripts;

public class DialogEditorWindow : EditorWindow
{

    [MenuItem("Tools/Dialog Editor")]
    public static void DialogEditor()
    {
        var window = GetWindow<DialogEditorWindow>();
        window.titleContent = new GUIContent("Dialog Editor");

    }

    private List<DialogNode> nodes;
    private List<DialogConnection> connections;

    private DialogNode selectedNode;
    private DialogConnectionPoint selectedInPoint;
    private DialogConnectionPoint selectedOutPoint;

    private Vector2 offset;
    private Vector2 drag;

    private Rect propertyAreaBounds;
    private Rect graphBounds;
    private void SetBounds()
    {
        propertyAreaBounds = new Rect(
            0, 0,
            (position.width * 0.25f),
            position.height);
        graphBounds = new Rect(
            propertyAreaBounds.x + propertyAreaBounds.width,
            propertyAreaBounds.y, position.width - propertyAreaBounds.width,
            position.height);
    }

    private void OnEnable()
    {
        SetBounds();
        InitStyles();
    }

    private GUIStyle nodeStyle;
    private GUIStyle selectedNodeStyle;
    private GUIStyle inPointStyle;
    private GUIStyle outPointStyle;
    private void InitStyles()
    {
        nodeStyle = new GUIStyle();
        nodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node1.png") as Texture2D;
        nodeStyle.border = new RectOffset(12, 12, 12, 12);

        selectedNodeStyle = new GUIStyle();
        selectedNodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node1 on.png") as Texture2D;
        selectedNodeStyle.border = new RectOffset(12, 12, 12, 12);

        inPointStyle = new GUIStyle();
        inPointStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn left.png") as Texture2D;
        inPointStyle.active.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn left on.png") as Texture2D;
        inPointStyle.border = new RectOffset(4, 4, 12, 12);

        outPointStyle = new GUIStyle();
        outPointStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn right.png") as Texture2D;
        outPointStyle.active.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn right on.png") as Texture2D;
        outPointStyle.border = new RectOffset(4, 4, 12, 12);
    }

    private void OnGUI()
    {
        SetBounds();
        DrawGrid();
        DrawNodes();
        DrawConnections();
        DrawConnectionLine(Event.current);
        ProcessEvents(Event.current);
        if (GUI.changed) Repaint();
        DrawPropertyArea();
    }

    private void DrawPropertyArea()
    {
        GUIStyle style = (GUIStyle)"Toolbar";
        GUI.Box(propertyAreaBounds, GUIContent.none);
        GUILayout.BeginArea(propertyAreaBounds, style);
        GUILayout.Label("Dialog Properties:");

        if (GUILayout.Button("Speichern"))
        {
            SaveOpenDialogChanges();
        }

        if(selectedNode != null)
        {
            GUILayout.BeginVertical();
            GUILayout.Label("Dialog Titel:");
            selectedNode.DialogTitel = GUILayout.TextField(selectedNode.DialogTitel);
            GUILayout.EndVertical();
        }
        GUILayout.EndArea();
    }

    private void DrawNodes()
    {
        if (nodes != null)
        {
            foreach (DialogNode node in nodes)
            {
                node.Draw();
            }
        }
    }

    private void DrawConnections()
    {
        if (connections != null)
        {
            for(int i = 0; i < connections.Count; i++)
            {
                connections[i].Draw();
            }

        }
    }

    private void DrawConnectionLine(Event e)
    {
        if(selectedInPoint != null && selectedOutPoint == null)
        {
            Handles.DrawBezier(
                selectedInPoint.rect.center,
                e.mousePosition,
                selectedInPoint.rect.center + Vector2.left * 50f,
                e.mousePosition - Vector2.left * 50f,
                Color.white,
                null, 2f);
        }

        if (selectedInPoint == null && selectedOutPoint != null)
        {
            Handles.DrawBezier(
                selectedOutPoint.rect.center,
                e.mousePosition,
                selectedOutPoint.rect.center - Vector2.left * 50f,
                e.mousePosition + Vector2.left * 50f,
                Color.white,
                null, 2f);
        }

        GUI.changed = true;
    }

    private void DrawGrid()
    {
        GUIStyle style = (GUIStyle)"TE BoxBackground";
        GUILayout.BeginArea(graphBounds, style);

        DrawGridLines(20, 0.2f, Color.white);
        DrawGridLines(100, 0.4f, Color.white);

        GUILayout.EndArea();
    }

    private void DrawGridLines(float gridSpacing, float gridOpacity, Color gridColor)
    {
        int widthDivs = Mathf.CeilToInt(graphBounds.width / gridSpacing);
        int heightDivs = Mathf.CeilToInt(graphBounds.height / gridSpacing);

        Handles.color = new Color(gridColor.r, gridColor.g, gridColor.b, gridOpacity);

        offset += drag * 0.5f;
        Vector3 newOffset = new Vector3(offset.x % gridSpacing, offset.y % gridSpacing, 0);

        for(int i = 0; i < widthDivs; i++)
        {
            Handles.DrawLine(new Vector3(gridSpacing * i, -gridSpacing, 0) + newOffset, new Vector3(gridSpacing * i, graphBounds.height, 0f) + newOffset);
        }


        for (int i = 0; i < heightDivs; i++)
        {
            Handles.DrawLine(new Vector3(-gridSpacing, gridSpacing * i, 0) + newOffset, new Vector3(graphBounds.width, gridSpacing * i, 0f) + newOffset);
        }

        Handles.color = Color.white;
    }

    private void ProcessEvents(Event e)
    {
        ProcessNodeEvents(e);
        drag = Vector2.zero;

        switch (e.type)
        {
            case EventType.MouseDown:
                if (e.button == 1)
                {
                    ProcessContextMenu(e.mousePosition);
                }
                break;
            case EventType.MouseDrag:
                if(e.button == 0)
                {
                    OnDrag(e.delta);
                }
                break;
        }
    }

    private void OnDrag(Vector2 delta)
    {
        drag = delta;

        if(nodes != null)
        {
            foreach(DialogNode node in nodes)
            {
                node.Drag(delta);
            }
        }

        GUI.changed = true;
    }

    private void ProcessNodeEvents(Event e)
    {
        if (nodes != null)
        {
            foreach (DialogNode node in nodes)
            {
                GUI.changed = node.ProcessEvents(e);
            }
        }
    }

    private void ProcessContextMenu(Vector2 mousePosition)
    {
        GenericMenu menu = new GenericMenu();
        menu.AddItem(new GUIContent("Add Dialog-Entry"), false, () => OnClickAddDialogEntry(mousePosition));
        menu.ShowAsContext();

    }

    private void OnSelectedNodeChanged(DialogNode node)
    {
        selectedNode = node;
    }

    private void OnClickAddDialogEntry(Vector2 position)
    {
        if (nodes == null)
        {
            nodes = new List<DialogNode>();
        }
        nodes.Add(new DialogNode(position, 200, 50, nodeStyle, selectedNodeStyle, inPointStyle,
            outPointStyle, OnClickInPoint,
            OnClickOutPoint, OnClickRemoveDialogNode,
            OnSelectedNodeChanged));
    }

    private void OnClickInPoint(DialogConnectionPoint point)
    {
        selectedInPoint = point;

        if (selectedOutPoint != null)
        {
            if (selectedOutPoint.node != selectedInPoint.node)
            {
                CreateConnection();
            }
            ClearConnectionSelection();
        }
    }

    private void OnClickOutPoint(DialogConnectionPoint point)
    {
        selectedOutPoint = point;

        if (selectedInPoint != null)
        {
            if (selectedOutPoint.node != selectedInPoint.node)
            {
                CreateConnection();
            }
            ClearConnectionSelection();
        }
    }

    private void OnClickRemoveConnection(DialogConnection connection)
    {
        connections?.Remove(connection);
    }

    private void OnClickRemoveDialogNode(DialogNode node)
    {
        connections?.RemoveAll((s) => s.inPoint == node.inPoint || s.outPoint == node.outPoint);
        nodes.Remove(node);
    }

    private void CreateConnection()
    {
        if (connections == null)
        {
            connections = new List<DialogConnection>();
        }
        connections.Add(new DialogConnection(
            selectedInPoint, selectedOutPoint, OnClickRemoveConnection));
    }

    private void ClearConnectionSelection()
    {
        selectedInPoint = null;
        selectedOutPoint = null;
    }

    #region "Saving"

    private Dialog selectedDialog;
    private void OnSelectionChange()
    {
        selectedDialog =  (Assets.Scripts.Dialogsystem.Models.Dialog)Selection.activeObject;
    }

    private void SaveOpenDialogChanges()
    {
        if(selectedDialog != null)
        {
            var entries = GetDialogEntries();
            var x = entries;
        }
    }

    private List<Assets.Scripts.Database.Dialogsystem.Models.DialogEntry> GetDialogEntries()
    {
        var rootNodes = (from entry in nodes
                           where connections.Where((s) => s.inPoint.node == entry).Count() == 0
                           select entry).ToList();
        if (rootNodes.Count == 0) return null;

        List<Assets.Scripts.Database.Dialogsystem.Models.DialogEntry> entries =
            new List<Assets.Scripts.Database.Dialogsystem.Models.DialogEntry>();
        foreach (DialogNode node in rootNodes)
        {
            Assets.Scripts.Database.Dialogsystem.Models.DialogEntry entry = new Assets.Scripts.Database.Dialogsystem.Models.DialogEntry()
            {
                EntryId = Guid.NewGuid(),
                DialogId = selectedDialog.DialogId,
                Position = node.rect.position,
                Titel = node.DialogTitel,
                Typ = Assets.Scripts.Database.Dialogsystem.Models.EntryTyp.Entry
            };
            entries.Add(entry);
            CreateSubEntries(entries, node, entry.EntryId);
        }

        return entries;

    }

    private void CreateSubEntries(List<Assets.Scripts.Database.Dialogsystem.Models.DialogEntry> list,
        DialogNode parent, Guid entryId)
    {
        var nodes = GetSubNodes(parent);
        foreach(DialogNode node in nodes)
        {
            Assets.Scripts.Database.Dialogsystem.Models.DialogEntry entry = new Assets.Scripts.Database.Dialogsystem.Models.DialogEntry()
            {
                EntryId = Guid.NewGuid(),
                DialogId = selectedDialog.DialogId,
                ParentId = entryId,
                Position = node.rect.position,
                Titel = node.DialogTitel,
                Typ = Assets.Scripts.Database.Dialogsystem.Models.EntryTyp.Entry
            };
            list.Add(entry);
            CreateSubEntries(list,node, entry.EntryId);
        }
    }

    private List<DialogNode> GetSubNodes(DialogNode parentNode)
    {
        return connections.Where((s) => s.outPoint.node == parentNode)
                .Select((s) => s.inPoint.node).ToList(); 
    }

    private class NodeData
    {
        public NodeData()
        {
            Nodes = new List<DialogNode>();
            Connections = new List<DialogConnection>();
        }
        public List<DialogNode> Nodes { get; set; }
        public List<DialogConnection> Connections { get; set; }
    }
    private  NodeData GetNodesFromDatabase()
    {
        if (selectedDialog != null) return null;
        NodeData data = new NodeData();
        using (Assets.Scripts.Database.Dialogsystem.DialogDatabaseEntities context =
            new Assets.Scripts.Database.Dialogsystem.DialogDatabaseEntities())
        {
            var entries = context.DialogEntriesSet.Find((s) => s.DialogId == selectedDialog.DialogId);
            
        }
        return null;
    }

    #endregion

}

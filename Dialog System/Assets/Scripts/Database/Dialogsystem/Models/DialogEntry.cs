using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using LiteDB;

namespace Assets.Scripts.Database.Dialogsystem.Models
{
    public class DialogEntry : ScriptableObject
    {
        public static string TableName = "tblDialogEntries";

        [BsonId]
        public Guid EntryId { get; set; }
        public Guid? ParentId { get; set; }
        public Guid DialogId { get; set; }
        public Guid ContentId { get; set; }

        public Vector2 Position { get; set; }

        public string Titel { get; set; }
        public EntryTyp Typ { get; set; }
    }

    public enum EntryTyp
    {
        Entry,
        Answer
    }
}


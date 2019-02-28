using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Database.Dialogsystem
{
    public class DialogDatabaseEntities : IDisposable
    {
        private const string DialogDatabaseAssetDirectory = "/Databases/Dialogsystem";
        private const string DialogDatabaseAssetFilename = "DialogDatabase.asset";
        private static void CreateDatabaseIfNecessary()
        {
            var dbString = GetDatabaseAssetPath();
            if (!File.Exists(dbString))
            {
                if (!AssetDatabase.IsValidFolder("Assets/Databases"))
                {
                    AssetDatabase.CreateFolder("Assets", "Databases");
                    AssetDatabase.CreateFolder("Assets/Databases", "Dialogsystem");
                }
                LiteDatabase database = ScriptableObject.CreateInstance<LiteDatabase>();
                AssetDatabase.CreateAsset(database, dbString);
                string path = AssetDatabase.GetAssetPath(database);
                Debug.Log(path);
                AssetDatabase.Refresh();

            }
        }

        private static string GetDatabaseAssetPath()
        {
            return "Assets" + DialogDatabaseAssetDirectory + "/" + DialogDatabaseAssetFilename;
        }

        private MemoryStream databaseStream;
        private LiteDB.LiteDatabase entities;
        private void CreateEntities()
        {
            CreateDatabaseIfNecessary();
            var dbString = GetDatabaseAssetPath();
            LiteDatabase databaseAsset = AssetDatabase.LoadAssetAtPath<LiteDatabase>(dbString);
            if (databaseAsset == null) return;
            databaseStream = ReadStreamData(databaseAsset.Data ?? new byte[] { });
            entities = new LiteDB.LiteDatabase(databaseStream);
            CreateTables(entities);
        }

        private MemoryStream ReadStreamData(byte[] data)
        {
            using (var temp = new MemoryStream(data))
            {
                var ms = new MemoryStream();
                temp.CopyTo(ms);

                return ms;
            }
        }

        public DialogDatabaseEntities()
        {
            CreateEntities();
        }

        private void CreateTables(LiteDB.LiteDatabase entities)
        {
            dialogEntriesSet = entities.GetCollection<Models.DialogEntry>();
        }

        private LiteDB.LiteCollection<Models.DialogEntry> dialogEntriesSet;
        public LiteDB.LiteCollection<Models.DialogEntry> DialogEntriesSet
        {
            get { return dialogEntriesSet; }
        }

        public void SaveChanges()
        {
            var dbString = GetDatabaseAssetPath();
            LiteDatabase databaseAsset = AssetDatabase.LoadAssetAtPath<LiteDatabase>(dbString);
            if (databaseAsset == null || databaseStream == null) return;
            databaseAsset.Data = databaseStream.ToArray();
        }

        public void Dispose()
        {
            dialogEntriesSet = null;
            databaseStream.Dispose();
            entities.Dispose();
        }
    }
}

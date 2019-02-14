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
    public class DialogDatabaseContextFactory
    {
        private const string DialogDatabaseAssetDirectory = "/Databases/Dialogsystem";
        private const string DialogDatabaseAssetFilename = "DialogDatabase.db";
        private static void CreateDatabaseIfNecessary()
        {
            var dbString = GetDatabaseAssetPath();
            if (!File.Exists(dbString))
            {
                if (!AssetDatabase.IsValidFolder("Assets/Databases"))
                {
                    AssetDatabase.CreateFolder("Assets", "Databases");
                    AssetDatabase.CreateFolder("Assets/Databases", "Dialogsystem");
                    LiteDatabase database = ScriptableObject.CreateInstance<LiteDatabase>();
                    AssetDatabase.CreateAsset(database, dbString);
                    Debug.Log(AssetDatabase.GetAssetPath(database));
                }
                AssetDatabase.Refresh();
            }
        }

        private static string GetDatabaseAssetPath()
        {
            return "Assets" + DialogDatabaseAssetDirectory + "/" + DialogDatabaseAssetFilename;
        }

        public static Func<LiteDB.LiteDatabase> GetContextCreator()
        {
            return () =>
            {
                CreateDatabaseIfNecessary();
                LiteDatabase databaseAsset = AssetDatabase.LoadAssetAtPath<LiteDatabase>(GetDatabaseAssetPath());
                if (databaseAsset == null) return null;
                MemoryStream ms = new MemoryStream(databaseAsset.Data);
                var db = new LiteDB.LiteDatabase(ms);
                
                return db;
            };
        }


    }
}

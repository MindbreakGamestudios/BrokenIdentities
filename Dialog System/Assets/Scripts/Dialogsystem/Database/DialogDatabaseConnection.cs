using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Dialogsystem.Database
{
    public class DialogDatabaseConnection
    {
        private class EmptyDatabaseObject
        {

        }

        public const string DialogDatabaseAssetDirectory = "/Databases/Dialogsystem";
        public const string DialogDatabaseAssetFilename = "DialogDatabase.db";
        public void GetDialogDataAdapter()
        {
            var dbString = Application.dataPath + DialogDatabaseAssetDirectory + DialogDatabaseAssetFilename;
            if (!File.Exists(dbString))
            {
                if (!AssetDatabase.IsValidFolder("Assets/Databases"))
                {
                    AssetDatabase.CreateFolder("Assets", "Databases");
                    AssetDatabase.CreateFolder("Assets/Databases", "Dialogsystem");
                }
                DialogDataAdapter.CreateDatabaseFile(dbString);
                AssetDatabase.Refresh();
            }
        }
    }

}

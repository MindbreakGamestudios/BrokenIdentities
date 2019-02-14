using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;

namespace Assets.Scripts.Dialogsystem.Database
{
    public class DialogDataAdapter
    {

        public static void CreateDatabaseFile(string path)
        {
            using (var db = new LiteDatabase(@path))
            {
                var col = db.GetCollection("Dialogs");
            }
        }

        public DialogDataAdapter(Func<LiteDatabase> dbFactory)
        {
            
        }

    }
}

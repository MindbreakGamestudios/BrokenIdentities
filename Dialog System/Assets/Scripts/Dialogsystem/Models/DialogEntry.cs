using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Dialogsystem.Models
{
    public class DialogEntry
    {
        public int Id;
        public int ParentId;
        public Guid DialogId;

        public string Titel;
        public string Text;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Dialogsystem.Models
{
    public class DialogTreeNode
    {
        public DialogEntry Entry;
        public List<DialogEntry> Entries = new List<DialogEntry>();
    }
}

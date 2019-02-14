using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Dialogsystem.Models
{
    [CreateAssetMenu(fileName = "NewDialog", menuName = "Dialog/New Dialog")]
    public class Dialog : ScriptableObject
    {
        public Guid DialogId;
        public string Titel;   
        
        public DialogTreeNode GetDialogStart()
        {
            return null;
        }
    }
}

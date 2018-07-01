using JMGames.Framework;
using JMGames.JMDialogs.Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace JMGames.Scripts.DialogSystem
{
    [Serializable]
    public class DialogItemDefinition : ScriptableObject
    {
        public Type InternalType;


        private DialogItem item;
        public DialogItem Item
        {
            get
            {
                if (item == null && InternalType != null)
                {
                    item = Activator.CreateInstance(InternalType) as DialogItem;
                }

                return item;
            }
            set
            {
                item = value;
            }
        }
    }
}

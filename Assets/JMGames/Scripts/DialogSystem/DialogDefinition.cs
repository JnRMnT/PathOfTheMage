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
    public class DialogDefinition : ScriptableObject
    {
        public Type InternalType;

        private Dialog item;
        public Dialog Item
        {
            get
            {
                if (item == null)
                {
                    item = Activator.CreateInstance(InternalType) as Dialog;
                }

                return item;
            }
            set
            {
                item = value;
                InternalType = value.GetType();
            }
        }
    }
}

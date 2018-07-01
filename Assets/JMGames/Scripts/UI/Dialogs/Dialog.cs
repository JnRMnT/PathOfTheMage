using JMGames.Common.Enums;
using JMGames.Framework;
using JMDialogsBase = JMGames.JMDialogs.Infrastructure.Base;
using System;
using JMGames.Scripts.DialogSystem;
using UnityEngine;
using JMGames.Scripts.UI.Dialogs.ContentInitializers;
using JMGames.Scripts.Managers;
using JMGames.JMDialogs.Pools.DialogPools;

namespace JMGames.Scripts.UI.Dialogs
{
    public class Dialog : JMBehaviour
    {
        public DialogTypeEnum Type;
        public DialogDefinition Definition;

        public override void DoStart()
        {
            Initialize();
            base.DoStart();
        }

        public void Initialize()
        {
            if (Definition == null || Definition.Item == null)
            {
                JMDialogsBase.Dialog randomDialog = DialogFactory.GetDialogPool(Type).PickOne();
                Definition = ScriptableObject.CreateInstance<DialogDefinition>();
                Definition.Item = randomDialog;
            }
        }
    }
}

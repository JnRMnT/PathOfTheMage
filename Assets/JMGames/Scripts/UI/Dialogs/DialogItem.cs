using JMGames.Common.Enums;
using JMGames.Framework;
using JMDialogsBase = JMGames.JMDialogs.Infrastructure.Base;
using JMGames.JMDialogs.Pools.DialogItemPools;
using System;
using JMGames.Scripts.DialogSystem;
using UnityEngine;
using JMGames.Scripts.UI.Dialogs.ContentInitializers;
using JMGames.Scripts.Managers;

namespace JMGames.Scripts.UI.Dialogs
{
    public class DialogItem : JMBehaviour
    {
        public DialogItemTypeEnum Type;

        public DialogItemDefinition Definition;

        public override void DoStart()
        {
            if (Definition == null || Definition.Item == null)
            {
                JMDialogsBase.DialogItem randomItem = DialogItemFactory.GetDialogItemPool(Type).PickOne();
                Definition = ScriptableObject.CreateInstance<DialogItemDefinition>();
                Definition.Item = randomItem;
            }

            GetComponentInChildren<DialogItemTitleInitializer>().Initialize(this);
            GetComponentInChildren<DialogItemContentInitializer>().Initialize(this);
            GetComponentInChildren<DialogItemRewardsInitializer>().Initialize(this);
            GetComponentInChildren<DialogItemObjectivesInitializer>().Initialize(this);            
            GetComponentInChildren<ResponseItemsInitializer>().Initialize(this);
            UIManager.Instance.SetCursorVisibility(true);
        }
    }
}

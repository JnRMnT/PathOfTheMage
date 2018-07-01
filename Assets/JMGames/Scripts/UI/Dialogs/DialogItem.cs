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

        public void Initialize()
        {
            if (Definition == null || Definition.Item == null)
            {
                JMDialogsBase.DialogItem randomItem = DialogItemFactory.GetDialogItemPool(Type).PickOne();
                Definition = ScriptableObject.CreateInstance<DialogItemDefinition>();
                Definition.Item = randomItem;
            }

            InitializeContent();
        }

        public void Initialize(JMDialogsBase.DialogItem item)
        {
            if (Definition == null)
            {
                Definition = ScriptableObject.CreateInstance<DialogItemDefinition>();
            }

            Definition.Item = item;
            InitializeContent();
        }

        private void InitializeContent()
        {
            GetComponentInChildren<DialogItemTitleInitializer>(true).Initialize(this);
            GetComponentInChildren<DialogItemContentInitializer>(true).Initialize(this);
            GetComponentInChildren<DialogItemRewardsInitializer>(true).Initialize(this);
            GetComponentInChildren<DialogItemObjectivesInitializer>(true).Initialize(this);
            GetComponentInChildren<ResponseItemsInitializer>(true).Initialize(this);
            UIManager.Instance.SetCursorVisibility(true);
        }
    }
}

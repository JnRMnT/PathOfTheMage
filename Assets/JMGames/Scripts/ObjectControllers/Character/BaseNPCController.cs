using JMGames.Scripts.Behaviours.Actions;
using JMGames.Scripts.EditorScripts;
using JMGames.Common.Entities;
using System;
using TMPro;
using UnityEngine;
using JMGames.Scripts.Managers;
using JMGames.Scripts.UI.Dialogs;
using BaseDialogs = JMGames.JMDialogs.Infrastructure.Base;
using JMGames.Common.Enums;

namespace JMGames.Scripts.ObjectControllers.Character
{
    public class BaseNPCController : Interactable
    {
        public TextMesh NameTagMesh;

        public NPCTypeEnum Type;
        [SerializeField]
#if UNITY_EDITOR
        [EnumFlagsAttribute]
#endif
        public NPCTradingTypeEnum TradingType;

        public override void DoStart()
        {
            //Set Name Tag
            NameTagMesh.text = NameTag;
            base.DoStart();
        }

        public string NPCName;
        private string npcName
        {
            get
            {
                if (string.IsNullOrEmpty(NPCName))
                {
                    NPCName = "";
                }

                return NPCName;
            }
        }

        public string NameTag
        {
            get
            {
                string nameTag = NPCName;
                if (!string.IsNullOrEmpty(NPCTitle))
                {
                    nameTag += " (" + NPCTitle + ")";
                }
                return nameTag;
            }
        }

        public string NPCTitle
        {
            get
            {
                switch (Type)
                {
                    case NPCTypeEnum.TalkingOnly:
                        return string.Empty;
                    case NPCTypeEnum.Trading:
                        string title = string.Empty;
                        foreach (NPCTradingTypeEnum value in Enum.GetValues(typeof(NPCTradingTypeEnum)))
                        {
                            if (value != NPCTradingTypeEnum.None && TradingType.HasFlag(value))
                            {
                                if (!string.IsNullOrEmpty(title))
                                {
                                    title += " & ";
                                }
                                title += LanguageManager.GetString("ENUM_NPCTRADINGTYPEENUM_" + value.ToString());
                            }
                        }
                        return title;
                }

                return string.Empty;
            }
        }

        public override void OnInteracted()
        {
            DialogManager.Instance.OnDialogEnded += OnDialogEnded;
            DialogManager.StartDialog(GetComponent<Dialog>().Definition.Item);
        }

        public void OnDialogEnded(BaseDialogs.Dialog dialog, DialogEndReasonType endReason)
        {
            DialogManager.Instance.OnDialogEnded -= OnDialogEnded;
            base.OnInteracted();
        }
    }
}

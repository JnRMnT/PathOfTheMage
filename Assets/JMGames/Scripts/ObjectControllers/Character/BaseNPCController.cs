using JMGames.Scripts.Behaviours.Actions;
using JMGames.Scripts.EditorScripts;
using JMGames.Scripts.Entities;
using System;
using TMPro;
using UnityEngine;

namespace JMGames.Scripts.ObjectControllers.Character
{
    public class BaseNPCController : Interactable
    {
        public TextMesh NameTagMesh;

        public NPCTypeEnum Type;
        [SerializeField]
        [EnumFlagsAttribute]
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

            base.OnInteracted();
        }
    }
}

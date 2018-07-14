using System;
using DuloGames.UI;
using JMGames.Framework;
using JMGames.Scripts.Spells;
using UnityEngine;
using UnityEngine.UI;

namespace JMGames.Scripts.UI
{
    public class SpellSlot : JMBehaviour
    {
        private UISpellSlot uiSpellSlot;
        private int spellSlot;

        public override void DoStart()
        {
            uiSpellSlot = GetComponent<UISpellSlot>();
            uiSpellSlot.iconGraphic.rectTransform.anchoredPosition = Vector2.zero;
            spellSlot = transform.GetSiblingIndex();
            base.DoStart();
        }

        public void HandleAssignment()
        {
            UISpellInfo spellInfo = uiSpellSlot.GetSpellInfo();
            BaseSpell selectedSpell = SpellManager.Instance.GetSpell(spellInfo.ID);
            SpellManager.Instance.SpellSlots[spellSlot] = selectedSpell;
            spellInfo.PowerCost = selectedSpell.ManaCost;
            spellInfo.Range = selectedSpell.MaxCastDistance;
            spellInfo.Cooldown = selectedSpell.Cooldown;
            spellInfo.CastTime = selectedSpell.CastTime;
            spellInfo.Name = LanguageManager.GetString("SPELL_" + selectedSpell.Name);
        }

        public void HandleUnassignment()
        {
            SpellManager.Instance.SpellSlots[spellSlot] = null;
        }

        public static bool HandleSpellTooltipAttributes(UISpellInfo spellInfo)
        {
            // Prepare some attributes
            if (spellInfo.Flags.Has(UISpellInfo_Flags.Passive))
            {
                UITooltip.AddLine("Passive");
            }
            else
            {
                // Power consumption
                if (spellInfo.PowerCost > 0f)
                {
                    UITooltip.AddLineColumn(spellInfo.PowerCost.ToString("0") + " " + LanguageManager.GetString("MANA"));
                }
                // Range
                if (spellInfo.Range > 0f)
                {
                    UITooltip.AddLineColumn(spellInfo.Range.ToString("0") + " " + LanguageManager.GetString("RANGE"));
                }

                // Cast time
                if (spellInfo.CastTime == 0f)
                    UITooltip.AddLineColumn("Instant");
                else
                    UITooltip.AddLineColumn(spellInfo.CastTime.ToString("0.0") + " " + LanguageManager.GetString("TOOLTIP_CASTTIME"));

                // Cooldown
                if (spellInfo.Cooldown > 0f)
                    UITooltip.AddLineColumn(spellInfo.Cooldown.ToString("0.0") +  " " + LanguageManager.GetString("TOOLTIP_COOLDOWN"));
            }
            return true;
        }
    }
}

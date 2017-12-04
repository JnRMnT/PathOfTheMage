using DuloGames.UI;
using JMGames.Framework;
using JMGames.Scripts.Spells;
using UnityEngine;
using UnityEngine.UI;

namespace JMGames.Scripts.UI.SpellBook
{
    public class SpellbookRow : JMBehaviour
    {
        [SerializeField] private UISpellSlot m_Slot;
        [SerializeField] private Text m_NameText;
        [SerializeField] private Text m_RankText;
        [SerializeField] private Text m_DescriptionText;

        public override void DoStart()
        {
            UISpellInfo[] spells = UISpellDatabase.Instance.spells;
            UISpellInfo uiSpell = spells[transform.GetSiblingIndex()];
            BaseSpell spell = SpellManager.Instance.GetSpell(uiSpell.ID);
            uiSpell.Cooldown = spell.Cooldown;
            uiSpell.PowerCost = spell.ManaCost;
            uiSpell.Range = spell.MaxCastDistance;
            uiSpell.CastTime = spell.CastTime;

            if (this.m_Slot != null) this.m_Slot.Assign(uiSpell);
            if (this.m_NameText != null) this.m_NameText.text = LanguageManager.GetString("SPELL_" + spell.Name);
            if (this.m_RankText != null) this.m_RankText.text = " " + Random.Range(1, 6).ToString();
            if (this.m_DescriptionText != null) this.m_DescriptionText.text = LanguageManager.GetString("DESCRIPTION_" + spell.name.Replace(" ", ""));

            base.DoStart();
        }
    }
}

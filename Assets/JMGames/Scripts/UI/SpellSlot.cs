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
            SpellManager.Instance.SpellSlots[spellSlot] = SpellManager.Instance.GetSpell(spellInfo.ID);
        }

        public void HandleUnassignment()
        {
            SpellManager.Instance.SpellSlots[spellSlot] = null;
        }
    }
}

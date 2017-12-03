using DuloGames.UI;
using JMGames.Framework;
using System;
using System.Collections;
using UnityEngine;

namespace JMGames.Scripts.UI.SpellBook
{
    public class SpellBook : JMBehaviour
    {
        public GameObject RowPrefab;

        public override void DoStart()
        {
            StartCoroutine(WaitAndInitializeSpellBook());
            base.DoStart();
        }

        private IEnumerator WaitAndInitializeSpellBook()
        {
            yield return new WaitUntil(() => { return SpellManager.Instance != null; });
            if(UISpellDatabase.Instance.spells.Length != SpellManager.Instance.AvailableSpells.Length)
            {
                throw new Exception("Spell databases does not match!");
            }
            for (int i = 0; i < SpellManager.Instance.AvailableSpells.Length; i++)
            {
                GameObject.Instantiate(RowPrefab, transform);
            }
        }
    }
}

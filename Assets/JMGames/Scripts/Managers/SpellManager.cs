using JMGames.Framework;
using JMGames.Scripts.ObjectControllers.Character;
using JMGames.Scripts.Spells;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpellManager : JMBehaviour
{
    public static SpellManager Instance;
    public List<SpellWrapper> SpellSlots;
    public BaseCharacterController CharacterController;

    public SpellWrapper[] AvailableSpells;
    public override void DoStart()
    {
        Instance = this;
        InitializeAvailableSpells();
        InitializeSpellSlots();
        CharacterController = GetComponent<BaseCharacterController>();
        base.DoStart();
    }

    protected void InitializeAvailableSpells()
    {
        foreach (SpellWrapper spell in AvailableSpells)
        {
            spell.Spell = (BaseSpell)Activator.CreateInstance(Type.GetType("JMGames.Scripts.Spells." + spell.Name));
        }
    }

    protected void InitializeSpellSlots()
    {
        SpellSlots = new List<SpellWrapper>()
        {
            GetSpell("LightningBolt")
        };
    }

    public SpellWrapper GetSpell(string name)
    {
        foreach (SpellWrapper availableSpell in AvailableSpells)
        {
            if (availableSpell.Spell.Name.Equals(name))
            {
                return availableSpell;
            }
        }

        return null;
    }

    public void CastSpell(int spellSlot)
    {
        GameObject spellInstance = GameObject.Instantiate(SpellSlots[spellSlot].Prefab);
        spellInstance.transform.position = new Vector3(MainPlayerController.Instance.transform.position.x, MainPlayerController.Instance.transform.position.y + 10, MainPlayerController.Instance.transform.position.z);

    }
}

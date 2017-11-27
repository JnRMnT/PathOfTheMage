using JMGames.Framework;
using JMGames.Scripts.ObjectControllers.Character;
using JMGames.Scripts.Spells;
using System.Collections;
using UnityEngine;

public class SpellManager : JMBehaviour
{
    public static SpellManager Instance;
    public BaseSpell[] SpellSlots;
    public BaseCharacterController CharacterController;

    public BaseSpell[] AvailableSpells;

    private int activeSpellSlot;
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
        if (AvailableSpells != null)
        {
            foreach (BaseSpell spell in AvailableSpells)
            {

            }
        }
    }

    protected void InitializeSpellSlots()
    {
        SpellSlots = new BaseSpell[10];
        SpellSlots[0] = GetSpell("LightningBolt");
    }

    public BaseSpell GetSpell(string name)
    {
        if (AvailableSpells != null)
        {
            foreach (BaseSpell availableSpell in AvailableSpells)
            {
                if (availableSpell.Name.Equals(name))
                {
                    return availableSpell;
                }
            }
        }

        return null;
    }

    public void CastSpell(int spellSlot)
    {
        if (SpellSlots[spellSlot] != null)
        {
            activeSpellSlot = spellSlot;
            MainPlayerController.Instance.Animator.SetTrigger((GetRandomSpellTriggerName()));
            DoCast();
        }
    }

    private string GetRandomSpellTriggerName()
    {
        if (activeSpellSlot != -1 && SpellSlots[activeSpellSlot] != null)
        {
            int castTriggerIndex = Mathf.RoundToInt(Random.Range(0, SpellSlots[activeSpellSlot].AnimationTriggerNames.Length - 1));
            return SpellSlots[activeSpellSlot].AnimationTriggerNames[castTriggerIndex];
        }

        return string.Empty;
    }

    public void DoCast()
    {
        if (activeSpellSlot != -1 && SpellSlots[activeSpellSlot] != null)
        {
            GameObject spellInstance = GameObject.Instantiate(SpellSlots[activeSpellSlot].Prefab);
            spellInstance.transform.position = MainPlayerController.Instance.RightHand.transform.position + transform.forward * 0.5f + transform.up * -2f;
            spellInstance.transform.rotation = MainPlayerController.Instance.transform.rotation;
            RFX4_EffectEvent effectEvent = MainPlayerController.Instance.GetComponent<RFX4_EffectEvent>();
            Transform handEffect = spellInstance.transform.Find("Hand");
            if (handEffect != null)
            {
                effectEvent.CharacterEffect = handEffect.gameObject;
            }
            Transform handEffect2 = spellInstance.transform.Find("Hand2");
            if (handEffect2 != null)
            {
                effectEvent.CharacterEffect2 = handEffect2.gameObject;
            }
            Transform effect = spellInstance.transform.Find("Effect");
            if (effect != null)
            {
                effectEvent.Effect = effect.gameObject;
            }
            Transform additionalEffect = spellInstance.transform.Find("Additional");
            if (additionalEffect != null)
            {
                effectEvent.AdditionalEffect = additionalEffect.gameObject;
            }

            spellInstance.SetActive(true);
            activeSpellSlot = -1;
        }
    }
}

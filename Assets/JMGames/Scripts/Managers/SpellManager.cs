using JMGames.Framework;
using JMGames.Scripts.Behaviours;
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

    private int activeSpellSlot = -1;

    public BaseSpell ActiveSpell
    {
        get
        {
            if (activeSpellSlot != -1 && SpellSlots[activeSpellSlot] != null)
            {
                return SpellSlots[activeSpellSlot];
            }
            else
            {
                return null;
            }
        }
        set
        {
            if (value == null)
            {
                activeSpellSlot = -1;
            }
        }
    }

    public bool HasActiveSpell
    {
        get
        {
            return ActiveSpell != null;
        }
    }

    public override void DoStart()
    {
        Instance = this;
        InitializeSpellSlots();
        CharacterController = GetComponent<BaseCharacterController>();
        base.DoStart();
    }

    protected void InitializeSpellSlots()
    {
        SpellSlots = new BaseSpell[10];
        SpellSlots[0] = GetSpell("LightningBolt");
        SpellSlots[1] = GetSpell("WaterDragon");
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
        if (SpellSlots[spellSlot] != null && !HasActiveSpell)
        {
            if (MainPlayerController.Instance.IsInAnimation("Free Movement") || (SpellSlots[spellSlot].Type == SpellTypeEnum.AOE && SpellSlots[spellSlot].AOEType == AOETypeEnum.SelectedArea))
            {
                //Do not wait for cooldown for area selection
                if (MainPlayerController.Instance.ManaPool.HasSufficientMana(SpellSlots[spellSlot].ManaCost))
                {
                    MainPlayerController.Instance.InputManager.AreaSelector.gameObject.SetActive(false);
                    activeSpellSlot = spellSlot;
                    StartCoroutine(WaitForAction());
                }
            }
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

    public void DoCast(Vector3 selectedAreaCenter)
    {
        if (ActiveSpell != null && MainPlayerController.Instance.ManaPool.HasSufficientMana(ActiveSpell.ManaCost))
        {
            GameObject spellInstance = GameObject.Instantiate(ActiveSpell.Prefab);
            spellInstance.transform.position = MainPlayerController.Instance.RightHand.transform.position + transform.forward * 0.5f + transform.up * -2f;
            spellInstance.transform.rotation = MainPlayerController.Instance.transform.rotation;
            if (ActiveSpell.Type == SpellTypeEnum.LinearCasting)
            {
                //Give camera aim for linear casting
                float xAngle = Camera.main.transform.rotation.eulerAngles.x - 15f;
                if (xAngle < 0)
                {
                    xAngle = 0;
                }
                spellInstance.transform.rotation = Quaternion.Euler(xAngle, MainPlayerController.Instance.transform.rotation.eulerAngles.y, MainPlayerController.Instance.transform.rotation.eulerAngles.z);
            }
            else if (ActiveSpell.Type == SpellTypeEnum.AOE && ActiveSpell.AOEType == AOETypeEnum.SelectedArea)
            {
                spellInstance.transform.position = selectedAreaCenter;
            }

            RFX4_TransformMotion[] transformMotions = spellInstance.GetComponentsInChildren<RFX4_TransformMotion>(true);
            if (transformMotions != null && transformMotions.Length > 0)
            {
                transformMotions[0].CollisionEnter += ActiveSpell.HandleCollision;
            }

            RFX4_EffectEvent effectEvent = MainPlayerController.Instance.GetComponent<RFX4_EffectEvent>();
            Transform handEffect = spellInstance.transform.Find("Hand");
            if (handEffect != null)
            {
                effectEvent.CharacterEffect = handEffect.gameObject;
            }
            else
            {
                effectEvent.CharacterEffect = null;
            }

            Transform handEffect2 = spellInstance.transform.Find("Hand2");
            if (handEffect2 != null)
            {
                effectEvent.CharacterEffect2 = handEffect2.gameObject;
            }
            else
            {
                effectEvent.CharacterEffect2 = null;
            }
            Transform effect = spellInstance.transform.Find("Effect");
            if (effect != null)
            {
                effectEvent.Effect = effect.gameObject;
            }
            else
            {
                effectEvent.Effect = null;
            }
            Transform additionalEffect = spellInstance.transform.Find("Additional");
            if (additionalEffect != null)
            {
                effectEvent.AdditionalEffect = additionalEffect.gameObject;
            }
            else
            {
                effectEvent.AdditionalEffect = null;
            }

            spellInstance.SetActive(true);
            MainPlayerController.Instance.ManaPool.UseMana(ActiveSpell.ManaCost);
            StartCoroutine(CastingCooldown());
        }
        else
        {
            ActiveSpell = null;
        }
    }

    protected IEnumerator CastingCooldown()
    {
        //Gets stuck for not being called
        yield return new WaitForSecondsRealtime(0.5f);
        activeSpellSlot = -1;
    }

    protected IEnumerator WaitForAction()
    {
        if (ActiveSpell != null)
        {
            if (ActiveSpell.RotationNeeded)
            {
                MainPlayerController.Instance.AlignWithCamera(SpellSlots[activeSpellSlot].CharacterRotationOffset);
                yield return new WaitUntil(() => { return MainPlayerController.Instance.IsAlignedWithCamera(); });
            }

            if (ActiveSpell != null)
            {
                bool delayCast = false;
                if (ActiveSpell.Type == SpellTypeEnum.AOE && ActiveSpell.AOEType == AOETypeEnum.SelectedArea)
                {
                    MainPlayerController.Instance.InputManager.InitializeAreaSelector(ActiveSpell);
                    delayCast = true;
                }

                if (!delayCast)
                {
                    TriggerAnimationAndCast(Vector3.zero);
                }
            }
        }
    }

    public bool TriggerAnimationAndCast(Vector3 selectedAreaCenter)
    {
        if (MainPlayerController.Instance.IsInAnimation("Free Movement"))
        {
            MainPlayerController.Instance.Animator.SetTrigger((GetRandomSpellTriggerName()));
            DoCast(selectedAreaCenter);
            return true;
        }
        else
        {
            if (ActiveSpell != null && (ActiveSpell.Type != SpellTypeEnum.AOE || ActiveSpell.AOEType != AOETypeEnum.SelectedArea))
            {
                ActiveSpell = null;
            }
            return false;
        }
    }
}

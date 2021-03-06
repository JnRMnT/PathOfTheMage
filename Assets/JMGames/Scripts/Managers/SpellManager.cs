﻿using DuloGames.UI;
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
    public UICastBar UICastBar;

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

    public UISpellSlot ActiveUISpellSlot
    {
        get
        {
            if (ActiveSpell != null)
            {
                return UISpellSlot.GetSlot(activeSpellSlot + 1, UISpellSlot_Group.Main_1);
            }
            else
            {
                return null;
            }
        }
    }

    public UISpellInfo ActiveUISpellInfo
    {
        get
        {
            if (ActiveUISpellSlot != null)
            {
                return ActiveUISpellSlot.GetSpellInfo();
            }
            else
            {
                return null;
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
        /*
        SpellSlots[0] = GetSpell("LightningBolt");
        SpellSlots[1] = GetSpell("WaterDragon");
        */
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

    public BaseSpell GetSpell(int uid)
    {

        if (AvailableSpells != null)
        {
            foreach (BaseSpell availableSpell in AvailableSpells)
            {
                if (availableSpell.UIID == uid)
                {
                    return availableSpell;
                }
            }
        }

        return null;
    }

    private void HandleCooldown()
    {
        // Handle cooldown just for the demonstration
        if (ActiveUISpellSlot.cooldownComponent != null && ActiveUISpellInfo.Cooldown > 0f)
        {
            // Start the cooldown on all the slots with the specified spell id
            foreach (UISpellSlot s in UISpellSlot.GetSlots())
            {
                if (s.IsAssigned() && s.GetSpellInfo() != null && s.cooldownComponent != null)
                {
                    // If the slot IDs match
                    if (s.GetSpellInfo().ID == ActiveUISpellInfo.ID)
                    {
                        // Start the cooldown
                        s.cooldownComponent.StartCooldown(ActiveUISpellInfo.ID, ActiveUISpellInfo.Cooldown);
                    }
                }
            }
        }
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

    protected bool IsInCooldown()
    {
        if (ActiveUISpellInfo.Cooldown > 0f && ActiveUISpellSlot.cooldownComponent != null && ActiveUISpellSlot.cooldownComponent.IsOnCooldown)
        {
            return true;
        }
        else
        {
            return false;
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
        if (ActiveSpell != null && MainPlayerController.Instance.ManaPool.HasSufficientMana(ActiveSpell.ManaCost) && !IsInCooldown())
        {
            UICastBar.StartCasting(ActiveUISpellInfo, ActiveUISpellInfo.CastTime, Time.time + ActiveUISpellInfo.CastTime);
            HandleCooldown();
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
            StartCoroutine(RecastCooldown());
        }
        else
        {
            ActiveSpell = null;
        }
    }

    protected IEnumerator RecastCooldown()
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

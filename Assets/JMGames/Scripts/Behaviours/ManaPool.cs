using System;
using JMGames.Framework;
using JMGames.Scripts.Behaviours.Attributes;
using System.Collections.Generic;

namespace JMGames.Scripts.Behaviours
{
    public class ManaPool : JMBehaviour
    {
        public List<IAttributeModifier> ActiveCapacityModifiers = new List<IAttributeModifier>();
        public List<IAttributeModifier> ActiveReplenishModifiers = new List<IAttributeModifier>();
        private float BaseManaCapacity = 1000f;
        private float manaCapacity = -1;
        public float ManaCapacity
        {
            get
            {
                if(manaCapacity == -1)
                {
                    CalculateManaCapacity();
                }

                return manaCapacity;
            }
        }

        public float CurrentMana;
        private float BaseReplenishAmountOverTime = 0.3f;

        public float ManaFloatingPercentage
        {
            get
            {
                if (CurrentMana == 0)
                {
                    return 0;
                }
                return CurrentMana / ManaCapacity;
            }
        }
        public float ManaPercentage
        {
            get
            {
                return ManaCapacity / 100 * CurrentMana;
            }
        }

        private void CalculateManaCapacity()
        {
            manaCapacity = BaseManaCapacity;
            float multiplier = 1f;
            foreach (IAttributeModifier modifier in ActiveCapacityModifiers)
            {
                manaCapacity += modifier.GetBaseAddition();
                multiplier += modifier.GetMultiplierAddition();
            }

            manaCapacity *= multiplier;
        }

        public override void DoStart()
        {
            CurrentMana = ManaCapacity;
            base.DoStart();
        }

        public override void DoFixedUpdate()
        {
            if (CurrentMana < ManaCapacity)
            {
                ReplenisManaOverTime();
            }
            base.DoFixedUpdate();
        }

        public bool HasSufficientMana(float cost)
        {
            return CurrentMana >= cost;
        }

        public bool UseMana(float cost)
        {
            CurrentMana -= cost;
            if (CurrentMana <= 0)
            {
                CurrentMana = 0;
            }
            return CurrentMana <= 0;
        }

        private void ReplenisManaOverTime()
        {
            float replenishAmount = BaseReplenishAmountOverTime;

            float multiplier = 1f;
            foreach (IAttributeModifier modifier in ActiveCapacityModifiers)
            {
                multiplier += modifier.GetMultiplierAddition();
            }
            replenishAmount *= multiplier;
            ReplenishMana(replenishAmount);
        }

        public bool ReplenishMana(float amount)
        {
            CurrentMana += amount;
            if (CurrentMana >= ManaCapacity)
            {
                CurrentMana = ManaCapacity;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void SetCapacityDirty()
        {
            manaCapacity = -1;
        }
    }
}

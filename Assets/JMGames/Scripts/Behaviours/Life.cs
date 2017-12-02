using JMGames.Framework;
using JMGames.Scripts.Behaviours.Attributes;
using System.Collections.Generic;
using System;

namespace JMGames.Scripts.Behaviours
{
    public class Life : JMBehaviour
    {
        public List<IAttributeModifier> ActiveMaxHealthModifiers = new List<IAttributeModifier>();
        public List<IAttributeModifier> ActiveReplenishModifiers = new List<IAttributeModifier>();


        private float baseMaxHealth = 100f;
        private float maxHealth = 1000f;
        /// <summary>
        /// Holds the object's default health
        /// </summary>
        public float MaxHealth
        {
            get
            {
                if (maxHealth == -1)
                {
                    CalculateMaxHealth();
                }

                return maxHealth;
            }
        }

        public float HealthFloatingPercentage
        {
            get
            {
                if (CurrentHealth == 0)
                {
                    return 0;
                }
                return CurrentHealth / MaxHealth;
            }
        }
        public float HealthPercentage
        {
            get
            {
                return MaxHealth / 100 * CurrentHealth;
            }
        }

        public bool AbleToReplenishHealth
        {
            get
            {
                //TODO:Implement cooldown after taking a hit
                return CurrentHealth < MaxHealth;
            }
        }
        
        public float BaseReplenishAmountOverTime = 0.5f;

        /// <summary>
        /// Holds the object's health
        /// </summary>
        public float CurrentHealth;

        /// <summary>
        /// Decreases health
        /// </summary>
        /// <param name="amount">Health decrease amount</param>
        /// <returns>If died</returns>
        public bool DecreaseHealth(float amount)
        {
            CurrentHealth -= amount;
            if (CurrentHealth <= 0)
            {
                CurrentHealth = 0;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IncreaseHealth(float amount)
        {
            this.CurrentHealth+= amount;
            if (CurrentHealth >= MaxHealth)
            {
                CurrentHealth = MaxHealth;
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void DoStart()
        {
            this.CurrentHealth = this.MaxHealth;
            base.DoStart();
        }

        public override void DoFixedUpdate()
        {
            ReplenishHealth();
            base.DoFixedUpdate();
        }

        private void ReplenishHealth()
        {
            if (AbleToReplenishHealth)
            {
                float replenishAmount = BaseReplenishAmountOverTime;
                float multiplier = 1f;
                foreach (IAttributeModifier modifier in ActiveMaxHealthModifiers)
                {
                    multiplier += modifier.GetMultiplierAddition();
                }
                replenishAmount *= multiplier;
                IncreaseHealth(replenishAmount);
            }
        }

        private void CalculateMaxHealth()
        {
            maxHealth = baseMaxHealth;
            float multiplier = 1f;
            foreach (IAttributeModifier modifier in ActiveMaxHealthModifiers)
            {
                maxHealth += modifier.GetBaseAddition();
                multiplier += modifier.GetMultiplierAddition();
            }

            maxHealth *= multiplier;
        }

        public void SetMaxHealthDirty()
        {
            maxHealth = -1;
        }
    }
}

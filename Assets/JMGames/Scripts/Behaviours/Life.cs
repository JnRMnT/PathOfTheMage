
using JMGames.Framework;

namespace JMGames.Scripts.Behaviours
{
    public class Life : JMBehaviour
    {

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
        private float BaseReplenishAmountOverTime = 0.001f;

        /// <summary>
        /// Holds the object's health
        /// </summary>
        public float CurrentHealth;

        /// <summary>
        /// Holds the object's default health
        /// </summary>
        public float MaxHealth;

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
            this.CurrentHealth++;
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
                IncreaseHealth(BaseReplenishAmountOverTime);
            }
        }
    }
}

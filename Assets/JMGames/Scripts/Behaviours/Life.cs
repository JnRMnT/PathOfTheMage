
using JMGames.Framework;

namespace JMGames.Scripts.Behaviours
{
    public class Life : JMBehaviour
    {
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
    }
}

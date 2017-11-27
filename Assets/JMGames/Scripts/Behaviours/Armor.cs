using JMGames.Framework;
using UnityEngine;

namespace JMGames.Scripts.Behaviours
{
    [RequireComponent(typeof(Life))]
    public class Armor : JMBehaviour
    {
        /// <summary>
        /// Holds the object's armor
        /// </summary>
        public float CurrentArmor;

        /// <summary>
        /// Holds the object's default Armor
        /// </summary>
        public float MaxArmor;

        /// <summary>
        /// Decreases Armor
        /// </summary>
        /// <param name="amount">Armor decrease amount</param>
        /// <returns>If died</returns>
        public bool DecreaseArmor(float amount)
        {
            CurrentArmor -= amount;
            if (CurrentArmor <= 0)
            {
                bool isDead = GetComponent<Life>().DecreaseHealth(CurrentArmor * -1);
                CurrentArmor = 0;
                return isDead;
            }
            else
            {
                return false;
            }
        }

        public bool IncreaseArmor(float amount)
        {
            this.CurrentArmor++;
            if (CurrentArmor >= MaxArmor)
            {
                CurrentArmor = MaxArmor;
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void DoStart()
        {
            this.CurrentArmor = this.MaxArmor;
            base.DoStart();
        }
    }
}

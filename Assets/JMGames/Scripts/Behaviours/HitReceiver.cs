
using JMGames.Framework;
using JMGames.Scripts.Spells;
using UnityEngine;

namespace JMGames.Scripts.Behaviours
{
    [RequireComponent(typeof(Life))]
    public class HitReceiver : JMBehaviour
    {
        /// <summary>
        /// Receives the damage and handles armor/health reduction.
        /// </summary>
        /// <param name="damage">Damage amount</param>
        /// <returns>If character died or not</returns>
        public bool ReceiveHit(HitInfo hitInfo)
        {
            float hitDamage = CalculateActualDamage(hitInfo);
            Armor armor = GetComponent<Armor>();
            if (armor != null)
            {
                return armor.DecreaseArmor(hitDamage);
            }
            else
            {
                return GetComponent<Life>().DecreaseHealth(hitDamage);
            }
        }

        /// <summary>
        /// Calculates received damage based on hit info
        /// </summary>
        /// <param name="hitInfo"></param>
        /// <returns>Actual damage to be reduced from health/armor</returns>
        protected float CalculateActualDamage(HitInfo hitInfo)
        {
            return hitInfo.BaseDamage;
        }
    }
}

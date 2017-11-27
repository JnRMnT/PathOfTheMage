
using JMGames.Framework;
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
        public bool ReceiveHit(float damage)
        {
            Armor armor = GetComponent<Armor>();
            if (armor != null)
            {
                return armor.DecreaseArmor(damage);
            }
            else
            {
                return GetComponent<Life>().DecreaseHealth(damage);
            }
        }
    }
}

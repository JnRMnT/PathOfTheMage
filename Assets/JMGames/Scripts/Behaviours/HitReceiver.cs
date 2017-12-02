
using JMGames.Framework;
using JMGames.Scripts.Spells;
using UnityEngine;

namespace JMGames.Scripts.Behaviours
{
    [RequireComponent(typeof(Life), typeof(Animator))]
    public class HitReceiver : JMBehaviour
    {
        public Life Life;
        public Animator Animator;
        public override void DoStart()
        {
            Life = GetComponent<Life>();
            Animator = GetComponent<Animator>();
            base.DoStart();
        }

        /// <summary>
        /// Receives the damage and handles armor/health reduction.
        /// </summary>
        /// <param name="damage">Damage amount</param>
        /// <returns>If character died or not</returns>
        public bool ReceiveHit(HitInfo hitInfo)
        {
            Animator.SetTrigger("TakeHit");
            Animator.SetFloat("ReceivedHitX", hitInfo.RelativeHitPoint.x);
            Animator.SetFloat("ReceivedHitY", hitInfo.RelativeHitPoint.y);
            float hitDamage = CalculateActualDamage(hitInfo);
            Armor armor = GetComponent<Armor>();
            if (armor != null)
            {
                return armor.DecreaseArmor(hitDamage);
            }
            else
            {
                return Life.DecreaseHealth(hitDamage);
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

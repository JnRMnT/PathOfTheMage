
using JMGames.Framework;
using JMGames.Scripts.Constants;
using JMGames.Scripts.Spells;
using JMGames.Scripts.Utilities;
using UnityEngine;
using UnityEngine.AI;

namespace JMGames.Scripts.Behaviours
{
    public class HitReceiver : JMBehaviour
    {
        public bool IsImmortal = false;
        public Life Life;
        public Animator Animator;
        public override void DoStart()
        {
            if (Life == null)
            {
                Life = GetComponent<Life>();
            }
            if (Animator == null)
            {
                Animator = GetComponent<Animator>();
            }
            base.DoStart();
        }

        /// <summary>
        /// Receives the damage and handles armor/health reduction.
        /// </summary>
        /// <param name="damage">Damage amount</param>
        /// <returns>If character died or not</returns>
        public bool ReceiveHit(HitInfo hitInfo)
        {
            Animator.SetTrigger(AnimationConstants.TakeHitTriggerName);
            Animator.SetFloat(AnimationConstants.ReceivedHitXParameter, hitInfo.RelativeHitPoint.x);
            Animator.SetFloat(AnimationConstants.ReceivedHitYParameter, hitInfo.RelativeHitPoint.y);
            float hitDamage = CalculateActualDamage(hitInfo);
            Armor armor = GetComponent<Armor>();
            bool died = false;
            if (armor != null)
            {
                died = armor.DecreaseArmor(hitDamage);
            }
            else
            {
                died = Life.DecreaseHealth(hitDamage);
            }

            if (died)
            {
                HandleDying();
                Animator.SetTrigger(AnimationConstants.DieTrigger);
            }

            return died;
        }

        protected virtual void HandleDying()
        {
            if (IsImmortal)
            {
                return;
            }

            GameObjectUtilities.DisableObjectBehaviours(gameObject);
            GameObjectUtilities.DisableColliders(gameObject);
            NavMeshAgent navMeshAgent = GetComponent<NavMeshAgent>();
            if (navMeshAgent != null)
            {
                navMeshAgent.enabled = false;
            }

            NavMeshObstacle navMeshObstacle = GetComponent<NavMeshObstacle>();
            if (navMeshObstacle != null)
            {
                navMeshObstacle.enabled = false;
            }

            Rigidbody rigidbody = GetComponent<Rigidbody>();
            if (rigidbody != null)
            {
                rigidbody.useGravity = false;
                rigidbody.isKinematic = true;
            }

            this.SendMessage("Die", this, SendMessageOptions.DontRequireReceiver);
            DestroyDeadCharacter deadCharacterComponent = this.gameObject.AddComponent<DestroyDeadCharacter>();
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

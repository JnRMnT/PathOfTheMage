using JMGames.Framework;
using JMGames.Scripts.Behaviours;
using JMGames.Scripts.Behaviours.Actions;
using JMGames.Scripts.Constants;
using JMGames.Scripts.Utilities;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace JMGames.Scripts.ObjectControllers.Character
{
    public class BaseEnemyController : JMBehaviour, IPhysicsBasedHitHandler
    {
        public float Damage = 50f;
        public Animator Animator;
        public NavMeshAgent NavMeshAgent;
        private Transform currentTarget;
        protected bool checkHit = false;
        private BattleCryBehaviour BattleCryBehaviour;

        /// <summary>
        ///  Vector3 position used to override animation root transform
        /// </summary>
        private Vector3 lastPosition;

        public override void DoLateUpdate()
        {
            transform.position = lastPosition;
            base.DoLateUpdate();
        }

        public override void DoStart()
        {
            BattleCryBehaviour = GetComponent<BattleCryBehaviour>();
            base.DoStart();
        }

        public override void DoUpdate()
        {
            lastPosition = transform.position;
            if (IsPlayerInRange())
            {
                RotateTowards(currentTarget);
                HitPlayer();
            }
            else
            {
                CheckForPlayers();
            }

            float velocity = NavMeshAgent.velocity.magnitude;
            if (velocity > 1)
            {
                velocity = 1;
            }
            Animator.SetFloat(AnimationConstants.VelocityParameter, velocity);
            base.DoUpdate();
        }

        protected virtual void HitPlayer()
        {
            float randomHit = Random.Range(0f, 1f);
            StartCoroutine(EnableCheckHit());
            Animator.SetFloat(AnimationConstants.RandomHitParameter, randomHit);
        }

        public virtual void Die()
        {
            Animator.SetFloat(AnimationConstants.RandomHitParameter, 0);
        }

        protected IEnumerator EnableCheckHit()
        {
            yield return new WaitUntil(() => { return !Animator.GetCurrentAnimatorStateInfo(AnimationConstants.EnemyHitLayer).IsName(AnimationConstants.EmptyStateName); });
            checkHit = true;
            yield return new WaitForSeconds(2f);
            if (Animator.GetCurrentAnimatorStateInfo(AnimationConstants.EnemyHitLayer).IsName(AnimationConstants.EmptyStateName))
            {
                checkHit = false;
            }
        }

        public void GiveHit(Collider collider, Vector3 hitPoint)
        {
            if (checkHit && collider.transform.GetInstanceID() == currentTarget.GetInstanceID() && enabled)
            {
                HitReceiver[] hitReceivers = currentTarget.GetComponentsInChildren<HitReceiver>();
                if (hitReceivers != null && hitReceivers.Length > 0)
                {
                    hitReceivers[0].ReceiveHit(new Spells.HitInfo
                    {
                        BaseDamage = Damage,
                        RelativeHitPoint = GameObjectUtilities.FindPointRelativeToObject(hitPoint, collider.gameObject)
                    });
                }
            }
        }

        protected void CheckForPlayers()
        {
            Collider[] players = Physics.OverlapSphere(transform.position, AIConstants.EnemyCheckRadius, RaycastingUtilities.CreateLayerMask(false, LayerMask.NameToLayer(LayerConstants.Player)));
            if (players != null && players.Length > 0)
            {
                if (BattleCryBehaviour != null)
                {
                    if (!BattleCryBehaviour.IsCompleted && !BattleCryBehaviour.IsActive)
                    {
                        BattleCryBehaviour.Play(this);
                    }
                    else if(BattleCryBehaviour.IsCompleted)
                    {
                        MoveTowardsAPlayer(players);
                    }
                }
                else
                {
                    MoveTowardsAPlayer(players);
                }
            }
        }

        private void MoveTowardsAPlayer(Collider[] players)
        {
            Collider player = GetTargetPlayer(players);
            NavMeshAgent.SetDestination(player.transform.position);
            currentTarget = player.transform;
        }

        protected virtual bool IsPlayerInRange()
        {
            if (currentTarget != null && Vector3.Distance(currentTarget.position, transform.position) < NavMeshAgent.stoppingDistance + 0.1f)
            {
                return true;
            }

            return false;
        }


        protected void RotateTowards(Transform target)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * CommonConstants.RotationSpeed);
        }


        /// <summary>
        /// Could be used later on a multiplayer phase to select from multiple players
        /// </summary>
        /// <param name="players"></param>
        private static Collider GetTargetPlayer(Collider[] players)
        {
            return players[0];
        }
    }
}

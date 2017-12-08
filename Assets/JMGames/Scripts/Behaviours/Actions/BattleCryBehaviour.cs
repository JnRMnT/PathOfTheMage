using System;
using JMGames.Framework;
using UnityEngine;
using JMGames.Scripts.ObjectControllers.Character;
using JMGames.Scripts.Constants;
using System.Collections;

namespace JMGames.Scripts.Behaviours.Actions
{
    public class BattleCryBehaviour : JMBehaviour
    {
        public bool IsCompleted, IsActive;

        public void Play(BaseEnemyController enemyController)
        {
            IsActive = true;
            enemyController.Animator.SetTrigger(AnimationConstants.BattleCryTrigger);
            StartCoroutine(WaitForCompletion(enemyController));
        }

        private IEnumerator WaitForCompletion(BaseEnemyController enemyController)
        {
            yield return new WaitForSeconds(1f);
            yield return new WaitUntil(() =>
            {
                return !enemyController.Animator.GetCurrentAnimatorStateInfo(AnimationConstants.EnemyBaseLayer).IsName(AnimationConstants.BattleCryStateName);
            });
            IsActive = false;
            IsCompleted = true;
        }
    }
}

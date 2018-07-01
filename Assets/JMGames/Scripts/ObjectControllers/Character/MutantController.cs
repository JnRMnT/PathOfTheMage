using JMGames.Scripts.Behaviours;
using JMGames.Scripts.Behaviours.Actions;
using JMGames.Common.Entities;
using JMGames.Scripts.Utilities;
using UnityEngine;

namespace JMGames.Scripts.ObjectControllers.Character
{
    public class MutantController : BaseEnemyController, IRewardGiver
    {
        public Reward[] GiveRewards()
        {
            return new Reward[]
            {
                new Reward
                {
                    Amount = 1000,
                    Type = RewardTypeEnum.Experience
                }
            };
        }
    }
}

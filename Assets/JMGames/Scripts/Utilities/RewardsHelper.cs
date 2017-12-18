using JMGames.Scripts.Entities;
using JMGames.Scripts.ObjectControllers.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JMGames.Scripts.Utilities
{
    public static class RewardsHelper
    {
        public static void GiveReward(Reward reward, BaseCharacterController targetCharacter)
        {
            switch (reward.Type)
            {
                case RewardTypeEnum.Experience:
                    if(targetCharacter.Experience != null)
                    {
                        targetCharacter.Experience.GainExperience(reward.Amount);
                    }
                    break;
                case RewardTypeEnum.Item:

                    break;
            }
        }

        public static void GiveRewards(Reward[] rewards, BaseCharacterController targetCharacter)
        {
            if (rewards != null)
            {
                foreach (Reward reward in rewards)
                {
                    GiveReward(reward, targetCharacter);
                }
            }
        }
    }
}

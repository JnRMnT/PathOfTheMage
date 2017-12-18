using JMGames.Scripts.Entities;
using JMGames.Scripts.ObjectControllers.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JMGames.Scripts.Behaviours
{
    public interface IRewardGiver
    {
        Reward[] GiveRewards();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JMGames.Scripts.Entities
{
    public class Reward
    {
        public RewardTypeEnum Type { get; set; }
        public object Item { get; set; }
        public int Amount { get; set; }
    }
}

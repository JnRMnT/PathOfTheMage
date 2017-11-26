using UnityEngine;

namespace JMGames.Scripts.Spells
{
    public class LightningBolt : BaseSpell
    {
        public override string AnimationStateName
        {
            get
            {
                return base.AnimationStateName;
            }
        }

        public override string Name
        {
            get
            {
                return "LightningBolt";
            }
        }
    }
}
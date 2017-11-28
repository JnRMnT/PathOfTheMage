using UnityEngine;

namespace JMGames.Scripts.Spells
{
    public class LightningBolt : BaseSpell
    {
        public override string[] AnimationTriggerNames
        {
            get
            {
                return new string[] { "CastSpell2H1" };
            }
        }

        public override string Name
        {
            get
            {
                return "LightningBolt";
            }
        }

        public override SpellTypeEnum Type
        {
            get
            {
                return base.Type;
            }
        }
    }
}
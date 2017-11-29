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

        public override float ManaCost
        {
            get
            {
                return 50f;
            }
        }

        public override string Name
        {
            get
            {
                return "LightningBolt";
            }
        }

        public override float Damage
        {
            get
            {
                return 10f;
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
using JMGames.Framework;

namespace JMGames.Scripts.Behaviours.Attributes
{
    public class ManaRegeneration: BaseAttribute, IAttributeModifier
    {
        public ManaPool ManaPool;
        public float GetBaseAddition()
        {
            return 0f;
        }

        public float GetMultiplierAddition()
        {
            return CurrentLevel / 25f;
        }

        public override void DoStart()
        {
            base.DoStart();
            ManaPool.ActiveReplenishModifiers.Add(this);
        }
    }
}

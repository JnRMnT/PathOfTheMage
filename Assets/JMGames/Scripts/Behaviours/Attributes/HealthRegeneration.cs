using JMGames.Framework;
using JMGames.Scripts.ObjectControllers.Character;

namespace JMGames.Scripts.Behaviours.Attributes
{
    public class HealthRegeneration : BaseAttribute, IAttributeModifier
    {
        public Life CharacterLife;
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
            CharacterLife.ActiveReplenishModifiers.Add(this);
        }
    }
}

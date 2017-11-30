namespace JMGames.Scripts.Behaviours.Attributes
{
    public class MaximumMana: BaseAttribute, IAttributeModifier
    {
        public ManaPool CharacterManaPool;

        public float GetBaseAddition()
        {
            return 50 * CurrentLevel;
        }

        public float GetMultiplierAddition()
        {
            if (CurrentLevel < 25)
            {
                return 0;
            }
            else
            {
                return CurrentLevel / 25;
            }
        }

        public override void LevelUp()
        {
            base.LevelUp();
            CharacterManaPool.SetCapacityDirty();
        }

        public override void DoStart()
        {
            base.DoStart();
            CharacterManaPool.ActiveCapacityModifiers.Add(this);
        }
    }
}

namespace JMGames.Scripts.Behaviours.Attributes
{
    public class MaximumHealth : BaseAttribute, IAttributeModifier
    {
        public Life CharacterLife;

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
            CharacterLife.SetMaxHealthDirty();
        }

        public override void DoStart()
        {
            base.DoStart();
            CharacterLife.ActiveMaxHealthModifiers.Add(this);
        }
    }
}

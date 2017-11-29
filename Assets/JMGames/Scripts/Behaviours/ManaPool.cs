using JMGames.Framework;

namespace JMGames.Scripts.Behaviours
{
    public class ManaPool : JMBehaviour
    {
        public float ManaCapacity = 100f;
        public float CurrentMana;
        private float BaseReplenishAmountOverTime = 0.3f;

        public float ManaFloatingPercentage
        {
            get
            {
                if (CurrentMana == 0)
                {
                    return 0;
                }
                return CurrentMana / ManaCapacity;
            }
        }
        public float ManaPercentage
        {
            get
            {
                return ManaCapacity / 100 * CurrentMana;
            }
        }

        public override void DoStart()
        {
            CurrentMana = ManaCapacity;
            base.DoStart();
        }

        public override void DoFixedUpdate()
        {
            if (CurrentMana < ManaCapacity)
            {
                ReplenisManaOverTime();
            }
            base.DoFixedUpdate();
        }

        public bool HasSufficientMana(float cost)
        {
            return CurrentMana >= cost;
        }

        public bool UseMana(float cost)
        {
            CurrentMana -= cost;
            if (CurrentMana <= 0)
            {
                CurrentMana = 0;
            }
            return CurrentMana <= 0;
        }

        private void ReplenisManaOverTime()
        {
            float replenishAmount = BaseReplenishAmountOverTime;
            //TODO: calculate additional mana regen modifiers here

            ReplenishMana(replenishAmount);
        }

        public bool ReplenishMana(float amount)
        {
            CurrentMana += amount;
            if (CurrentMana >= ManaCapacity)
            {
                CurrentMana = ManaCapacity;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

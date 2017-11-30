using JMGames.Framework;
using JMGames.Scripts.ObjectControllers.Character;

namespace JMGames.Scripts.Behaviours.Attributes
{
    public class BaseAttribute: JMBehaviour
    {
        public int CurrentLevel;
        protected BaseCharacterController Character;
        public override void DoStart()
        {
            Character = GetComponent<BaseCharacterController>();
            base.DoStart();
        }

        public virtual void LevelUp()
        {
            CurrentLevel++;
        }
    }
}

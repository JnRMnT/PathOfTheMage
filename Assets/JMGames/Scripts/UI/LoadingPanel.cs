using JMGames.Framework;

namespace JMGames.Scripts.UI
{
    public class LoadingPanel: JMBehaviour
    {
        public static LoadingPanel Instance;
        public override void DoStart()
        {
            Instance = this;
            base.DoStart();
        }
    }
}

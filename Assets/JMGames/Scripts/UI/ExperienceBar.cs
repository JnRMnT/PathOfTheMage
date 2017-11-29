using DuloGames.UI;
using JMGames.Framework;

namespace JMGames.Scripts.UI
{
    public class ExperienceBar : JMBehaviour
    {
        public UIProgressBar ProgressBar;
        public static ExperienceBar Instance;
        public override void DoStart()
        {
            Instance = this;
            base.DoStart();
        }

        public void UpdateBar(float percentage)
        {
            ProgressBar.fillAmount = percentage;
        }
    }
}

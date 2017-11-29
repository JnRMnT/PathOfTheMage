using JMGames.Framework;
using JMGames.Scripts.ObjectControllers.Character;
using UnityEngine.UI;

namespace JMGames.Scripts.UI
{
    public class HealthBar : JMBehaviour
    {
        public Image HealthImage;
        public override void DoUpdate()
        {
            HealthImage.fillAmount = MainPlayerController.Instance.Health.HealthFloatingPercentage;
            base.DoUpdate();
        }
    }
}

using JMGames.Framework;
using JMGames.Scripts.ObjectControllers.Character;
using UnityEngine.UI;

namespace JMGames.Scripts.UI
{
    public class ManaBar: JMBehaviour
    {
        public Image ManaImage;
        public override void DoUpdate()
        {
            if (MainPlayerController.Instance != null)
            {
                ManaImage.fillAmount = MainPlayerController.Instance.ManaPool.ManaFloatingPercentage;
            }
            base.DoUpdate();
        }
    }
}

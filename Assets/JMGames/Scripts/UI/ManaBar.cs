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
            ManaImage.fillAmount = MainPlayerController.Instance.ManaPool.ManaFloatingPercentage;
            base.DoUpdate();
        }
    }
}

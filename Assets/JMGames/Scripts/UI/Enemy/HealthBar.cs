using DuloGames.UI;
using JMGames.Framework;
using JMGames.Scripts.Behaviours;
using JMGames.Scripts.ObjectControllers.Character;
using UnityEngine;
using UnityEngine.UI;

namespace JMGames.Scripts.UI.Enemy
{
    public class HealthBar : JMBehaviour
    {
        public Text PercentageText;
        public Life Life;
        public UIProgressBar ProgressBar;

        public override void DoUpdate()
        {
            float percentage = (Life.CurrentHealth / Life.MaxHealth * 100);
            PercentageText.text = percentage.ToString() + "%";
            ProgressBar.fillAmount = percentage / 100;
            transform.rotation = Camera.main.transform.rotation;
            PercentageText.enabled = percentage > 0;
            base.DoUpdate();
        }
    }
}

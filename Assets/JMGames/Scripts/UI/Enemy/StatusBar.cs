using JMGames.Framework;
using JMGames.Scripts.Framework;
using JMGames.Scripts.Managers;

namespace JMGames.Scripts.UI.Enemy
{
    public class StatusBar : JMBehaviour, IPreferenceChangeListener
    {
        public ArmorBar ArmorBar;
        public HealthBar HealthBar;

        public override void DoStart()
        {
            HandlePreferenceChange();
            base.DoStart();
        }

        public void HandlePreferenceChange()
        {
            ArmorBar.gameObject.SetActive(PreferencesManager.ShowEnemyHealthBars);
            HealthBar.gameObject.SetActive(PreferencesManager.ShowEnemyHealthBars);
            gameObject.SetActive(PreferencesManager.ShowEnemyHealthBars);
        }
    }
}

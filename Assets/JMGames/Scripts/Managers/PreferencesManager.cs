using JMGames.Framework;
using UnityEngine;

namespace JMGames.Scripts.Managers
{
    public class PreferencesManager : JMBehaviour
    {
        public static bool ShowEnemyHealthBars
        {
            get
            {
                return PlayerPrefs.GetInt("ShowEnemyHealthBars", 1) == 1;
            }
            set
            {
                PlayerPrefs.SetInt("ShowEnemyHealthBars", value ? 1 : 0);
            }
        }
    }
}

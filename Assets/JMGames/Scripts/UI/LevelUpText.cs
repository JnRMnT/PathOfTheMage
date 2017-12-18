using JMGames.Scripts.ObjectControllers.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JMGames.Scripts.UI
{
    public class LevelUpText : NotificationText
    {
        public static LevelUpText Instance;
        public override void DoStart()
        {
            base.DoStart();
            Instance = this;
        }

        public void Show()
        {
            string level = LanguageManager.GetString("TITLE_LEVEL");
            base.ShowNotification("TITLE_LEVELUP", level + " " + MainPlayerController.Instance.Experience.CurrentLevel, true, false);
        }
    }
}

using System;
using JMGames.Framework;
using UnityEngine.UI;
using JMGames.Scripts.Behaviours.Common;

namespace JMGames.Scripts.UI
{
    public class NotificationText : JMBehaviour
    {
        public Text Header, Text;
        public DelayedDeActivate DelayedDeactivator;
        public static NotificationText Instance;

        public override void DoStart()
        {
            Instance = this;
            base.DoStart();
            Deactivate();
        }

        public void ShowNotification(string title, string text, bool titleIsResource = true, bool textIsResource = true)
        {
            if (titleIsResource)
            {
                title = LanguageManager.GetString(title);
            }
            if (textIsResource)
            {
                text = LanguageManager.GetString(text);
            }
            Header.text = title;
            Text.text = text;
            Activate();
            DelayedDeactivator.StartProcess();
        }
    }
}

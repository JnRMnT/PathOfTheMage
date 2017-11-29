using UnityEngine.UI;

namespace JMGames.Framework.Localization
{
    public class UITextLocalizer : JMBehaviour
    {
        public bool IsLocalizable = true;
        public string ResourceKey;

        public override void DoStart()
        {
            if (IsLocalizable)
            {
                this.GetComponent<Text>().text = LanguageManager.GetString(ResourceKey);
            }

            base.DoStart();
        }
    }
}
using UnityEngine;

namespace JMGames.Framework.Localization
{
    public class TextMeshLocalizer : JMBehaviour
    {
        public bool IsLocalizable = true;
        public string ResourceKey;

        public override void DoStart()
        {
            if (IsLocalizable)
            {
                this.GetComponent<TextMesh>().text = LanguageManager.GetString(ResourceKey);
            }

            base.DoStart();
        }
    }
}
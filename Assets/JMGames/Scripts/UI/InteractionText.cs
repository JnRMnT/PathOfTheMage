using JMGames.Framework;
using JMGames.Scripts.ObjectControllers.Character;

namespace JMGames.Scripts.UI
{
    public class InteractionText : JMBehaviour
    {
        public override void DoUpdate()
        {
            if (MainPlayerController.Instance.ActiveInteractable != null && !MainPlayerController.Instance.ActiveInteractable.IsCharacterLookingAtMe())
            {
                MainPlayerController.Instance.ActiveInteractable = null;
                Deactivate();
            }
            base.DoUpdate();
        }

        public void Interact()
        {
            MainPlayerController.Instance.InteractionKeyValid = true;
        }
    }
}

using JMGames.Framework;
using UnityEngine;
using System.Collections;
using JMGames.Scripts.ObjectControllers.Character;
using JMGames.Scripts.Managers;

namespace JMGames.Scripts.Behaviours.Actions
{
    public class Interactable : JMBehaviour
    {
        public float InteractionDistance = 3f;
        public bool Active = false;
        public bool HasBeenInteracted = false;
        public bool CanBeReInteracted = false;
        public BaseCharacterController TrackedCharacter;
        public override void DoStart()
        {
            if (TrackedCharacter == null)
            {
                StartCoroutine(SetTrackedCharacter());
            }
            base.DoStart();
        }

        private IEnumerator SetTrackedCharacter()
        {
            yield return new WaitUntil(() => { return MainPlayerController.Instance != null; });
            TrackedCharacter = MainPlayerController.Instance;
        }

        public override void DoUpdate()
        {
            if (TrackedCharacter != null && !Active && (!HasBeenInteracted || CanBeReInteracted))
            {
                if (IsCharacterCloseEnough() && IsCharacterLookingAtMe())
                {
                    StartCoroutine(StartInteraction());
                }
            }
            base.DoUpdate();
        }

        protected virtual bool IsCharacterCloseEnough()
        {
            return Vector3.Distance(TrackedCharacter.transform.position, transform.position) <= InteractionDistance;
        }

        public virtual bool IsCharacterLookingAtMe()
        {
            if (TrackedCharacter is MainPlayerController)
            {
                return (transform.forward - Camera.main.transform.forward).magnitude < 1.05f;
            }
            else
            {
                return true;
            } 
        }

        public virtual IEnumerator StartInteraction()
        {
            MainPlayerController.Instance.ActiveInteractable = this;
            UIManager.Instance.InteractionText.Activate();
            yield return new WaitUntil(() => { return IsInteractionReady(); });
            Active = true;
            UIManager.Instance.InteractionText.Deactivate();
            OnInteractionReady();
            yield return new WaitUntil(() => { return IsInteractionFeedbackCompleted(); });
            OnInteracted();
        }

        protected virtual void OnInteractionReady()
        {

        }

        private bool IsInteractionReady()
        {
            return TrackedCharacter.ActiveInteractable == this && TrackedCharacter.InteractionKeyValid;
        }

        protected virtual bool IsInteractionFeedbackCompleted()
        {
            return true;
        }

        public virtual void OnInteracted()
        {
            HasBeenInteracted = true;
            Active = false;
            TrackedCharacter.InteractionKeyValid = false;
            TrackedCharacter.ActiveInteractable = null;
        }
    }
}

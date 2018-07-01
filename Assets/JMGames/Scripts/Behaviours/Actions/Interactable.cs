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

        public bool InverseX = false, InverseY = false, InverseZ = false;

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

        public virtual bool IsCharacterCloseEnough()
        {
            return Vector3.Distance(TrackedCharacter.transform.position, transform.position) <= InteractionDistance;
        }

        public virtual bool IsCharacterLookingAtMe()
        {
            if (TrackedCharacter is MainPlayerController)
            {
                Vector3 cameraForward = Camera.main.transform.forward;
                Vector3 transformForward = transform.forward;
                if (InverseX)
                {
                    transformForward.x *= -1;
                }
                if (InverseY)
                {
                    transformForward.y *= -1;
                }
                if (InverseZ)
                {
                    transformForward.z *= -1;
                }
                Vector3 lookVector = transformForward + cameraForward;
                return lookVector.magnitude < 1.05f;
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

using System.Collections;
using UnityEngine;

namespace JMGames.Scripts.Behaviours.Actions.Interactables
{
    public class InteractableChest : Interactable
    {
        private float LidOpenSpeed = 100f;
        private bool IsAnimationCompleted = false;
        private int LidDirection = 1;
        public Transform Lid;
        private Vector3 rotation;
        
        protected override void OnInteractionReady()
        {
            GetComponent<AudioSource>().Play();
            rotation = transform.rotation.eulerAngles;
            StartCoroutine(PlayOpenLidAnimation());
            base.OnInteractionReady();
        }

        public override void DoUpdate()
        {
            if (Active)
            {
                rotation.x += Time.deltaTime * LidOpenSpeed * LidDirection;
                Lid.transform.rotation = Quaternion.Euler(rotation.x, rotation.y, rotation.z);
            }
            base.DoUpdate();
        }

        private IEnumerator PlayOpenLidAnimation()
        {
            yield return new WaitUntil(() => { return rotation.x >= 119; });
            LidOpenSpeed = 75f;
            LidDirection = -1;
            yield return new WaitUntil(() => { return rotation.x <= 114; });
            LidDirection = 1;
            yield return new WaitUntil(() => { return rotation.x >= 119; });
            LidDirection = -1;
            yield return new WaitUntil(() => { return rotation.x <= 114; });
            LidDirection = 1;
            yield return new WaitUntil(() => { return rotation.x >= 119; });
            LidDirection = -1;
            yield return new WaitUntil(() => { return rotation.x <= 114; });
            LidDirection = 1;
            yield return new WaitUntil(() => { return rotation.x >= 119; });
            IsAnimationCompleted = true;
        }

        protected override bool IsInteractionFeedbackCompleted()
        {
            return IsAnimationCompleted;
        }
    }
}

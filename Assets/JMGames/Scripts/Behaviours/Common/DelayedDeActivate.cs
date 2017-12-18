using JMGames.Framework;
using System.Collections;
using UnityEngine;

namespace JMGames.Scripts.Behaviours.Common
{
    public class DelayedDeActivate: JMBehaviour
    {
        public bool DeactivateOnStart = false;
        public float Delay = 3f;
        public override void DoStart()
        {
            if (DeactivateOnStart)
            {
                StartProcess();
            }
            base.DoStart();
        }

        public void StartProcess()
        {
            StartCoroutine(DelayedDeativation());
        }

        private IEnumerator DelayedDeativation()
        {
            yield return new WaitForSeconds(Delay);
            Deactivate();
        }
    }
}

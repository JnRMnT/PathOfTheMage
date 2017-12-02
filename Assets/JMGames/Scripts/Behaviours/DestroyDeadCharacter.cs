using JMGames.Framework;
using System.Collections;
using UnityEngine;

namespace JMGames.Scripts.Behaviours
{
    public class DestroyDeadCharacter : JMBehaviour
    {
        private Vector3 destination = new Vector3(-999, -999, -999);

        public override void DoStart()
        {
            StartCoroutine(WaitAndSetDestination());
            base.DoStart();
        }

        private IEnumerator WaitAndSetDestination()
        {
            yield return new WaitForSeconds(15f);
            destination = transform.position + Vector3.down * 5f;
        }

        public override void DoUpdate()
        {
            if (destination.x != -999 && destination.y != -999 && destination.z != -999)
            {
                if (Vector3.Distance(destination, transform.position) > 0.1f)
                {
                    transform.position = Vector3.Lerp(transform.position, destination, Time.deltaTime);
                }
                else
                {
                    Destroy(this.gameObject);
                }
            }
            base.DoUpdate();
        }
    }
}

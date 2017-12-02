using JMGames.Framework;
using UnityEngine;

namespace JMGames.Scripts.Behaviours
{
    public class PhysicsBasedHitTransmitter : JMBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            IPhysicsBasedHitHandler[] physicsBasedHitHandlers = transform.GetComponentsInParent<IPhysicsBasedHitHandler>();
            if (physicsBasedHitHandlers != null && physicsBasedHitHandlers.Length > 0)
            {
                foreach (IPhysicsBasedHitHandler handler in physicsBasedHitHandlers)
                {
                    handler.GiveHit(other, other.ClosestPointOnBounds(transform.position));
                }
            }
        }
    }
}

using UnityEngine;

namespace JMGames.Scripts.Behaviours
{
    public interface IPhysicsBasedHitHandler
    {
        void GiveHit(Collider collision, Vector3 hitPoint);
    }
}

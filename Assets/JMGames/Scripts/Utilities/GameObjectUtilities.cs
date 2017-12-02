using JMGames.Framework;
using UnityEngine;

namespace JMGames.Scripts.Utilities
{
    public class GameObjectUtilities
    {
        public static Vector3 FindPointRelativeToObject(Vector3 point, GameObject gameObject)
        {
            Collider[] colliders = gameObject.GetComponentsInChildren<Collider>(true);
            float minX = 99999,
                maxX = -99999,
                minY = 99999,
                maxY = -99999,
                minZ = 99999,
                maxZ = -99999;

            if (colliders != null && colliders.Length > 0)
            {
                Bounds bounds = colliders[0].bounds;
                if (bounds.max.x > maxX)
                {
                    maxX = bounds.max.x;
                }
                if (bounds.min.x < minX)
                {
                    minX = bounds.min.x;
                }
                if (bounds.max.y > maxY)
                {
                    maxY = bounds.max.y;
                }
                if (bounds.min.y < minY)
                {
                    minY = bounds.min.y;
                }
                if (bounds.max.z > maxZ)
                {
                    maxZ = bounds.max.z;
                }
                if (bounds.min.z < minZ)
                {
                    minZ = bounds.min.z;
                }
            }

            return new Vector3(((point.x - minX) / (maxX - minX)) * 2 - 1, ((point.y - minY) / (maxY - minY)) * 2 - 1, ((point.z - minZ) / (maxZ - minZ)) * 2 - 1);
        }
    }
}

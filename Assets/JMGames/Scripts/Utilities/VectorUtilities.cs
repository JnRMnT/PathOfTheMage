using UnityEngine;

namespace JMGames.Scripts.Utilities
{
    public class VectorUtilities
    {
        public static Vector3 GetPositiveEulerRotationAngles(Transform transform)
        {
            return GetPositiveEulerRotationAngles(transform.rotation.eulerAngles);
        }

        public static Vector3 GetPositiveEulerRotationAngles(Vector3 rotationEuler)
        {
            float x = rotationEuler.x;
            float y = rotationEuler.y;
            float z = rotationEuler.z;

            if (x < 0)
            {
                x += 360;
            }
            if (y < 0)
            {
                y += 360;
            }
            if (z < 0)
            {
                y += 360;
            }
            return new Vector3(x, y, z);
        }
    }
}

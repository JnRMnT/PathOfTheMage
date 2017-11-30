using JMGames.Framework;
using JMGames.Scripts.ObjectControllers.Character;
using UnityEngine;

namespace JMGames.Scripts.Behaviours
{
    public class Portal : JMBehaviour
    {
        public Transform Destination;
        public Vector3 DestinationVector;
        public Vector3 DestinationRotation;
        public void OnTriggerEnter(Collider other)
        {
            if (other.transform.tag == "Player")
            {
                if (Destination != null)
                {
                    MainPlayerController.Instance.transform.position = Destination.position;
                }
                else
                {
                    MainPlayerController.Instance.transform.position = DestinationVector;
                }

                if (DestinationRotation != Vector3.zero)
                {
                    MainPlayerController.Instance.transform.rotation = Quaternion.Euler(DestinationRotation);
                }
            }
        }
    }
}

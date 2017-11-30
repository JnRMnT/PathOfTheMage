using JMGames.Framework;
using JMGames.Scripts.ObjectControllers.Character;
using UnityEngine;

namespace JMGames.Scripts.UI
{
    public class Minimap : JMBehaviour
    {
        [Range(10, 100)]
        public float ZoomAmount = 50;
        public Camera MinimapCamera;
        public override void DoStart()
        {
            MinimapCamera.orthographicSize = ZoomAmount;
            base.DoStart();
        }

        public override void DoUpdate()
        {
            MinimapCamera.transform.position = new Vector3(MainPlayerController.Instance.transform.position.x, MainPlayerController.Instance.transform.position.y + 20f, MainPlayerController.Instance.transform.position.z);
            base.DoUpdate();
        }

        public void ZoomIn()
        {
            if (ZoomAmount > 10)
            {
                ZoomAmount -= 5f;
                if(ZoomAmount < 10)
                {
                    ZoomAmount = 10;
                }
                MinimapCamera.orthographicSize = ZoomAmount;
            }
        }

        public void ZoomOut()
        {
            if (ZoomAmount < 100)
            {
                ZoomAmount += 5f;
                if (ZoomAmount > 100)
                {
                    ZoomAmount = 100;
                }
                MinimapCamera.orthographicSize = ZoomAmount;
            }
        }
    }
}

using JMGames.Framework;
using UnityEngine;

namespace JMGames.Scripts.Managers
{
    public class UIManager : JMBehaviour
    {
        public Texture2D CursorTexture;
        public CursorMode CursorMode = CursorMode.Auto;
        public Vector2 HotSpot = Vector2.zero;
        public override void DoStart()
        {
            Cursor.SetCursor(CursorTexture, HotSpot, CursorMode);
            base.DoStart();
        }
    }
}

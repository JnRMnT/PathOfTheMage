using DuloGames.UI;
using JMGames.Framework;
using JMGames.Scripts.UI.Window;
using System.Collections.Generic;
using UnityEngine;

namespace JMGames.Scripts.Managers
{
    public class UIManager : JMBehaviour
    {
        public static UIManager Instance;
        public Texture2D CursorTexture;
        public CursorMode CursorMode = CursorMode.Auto;
        public Vector2 HotSpot = Vector2.zero;
        public override void DoStart()
        {
            Instance = this;
            Cursor.SetCursor(CursorTexture, HotSpot, CursorMode);
            base.DoStart();
        }

        public void OnApplicationFocus(bool focus)
        {
            if (!focus)
            {
                Cursor.visible = true;
                PauseMenuWindow.Instance.Pause();
            }
        }

        public void OnApplicationPause(bool pause)
        {
            if (pause)
            {
                Cursor.visible = true;
                PauseMenuWindow.Instance.Pause();
            }
        }

        public void SetCursorVisibility(bool isVisible)
        {
            Cursor.visible = isVisible;
            if (isVisible)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

        public bool HasActiveWindows()
        {
            List<UIWindow> windows = UIWindow.GetWindows();
            foreach(UIWindow window in windows)
            {
                if (window.IsVisible)
                {
                    return true;
                }
            }

            return false;
        }
    }
}

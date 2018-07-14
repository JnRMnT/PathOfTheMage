using DuloGames.UI;
using JMGames.Framework;
using JMGames.Scripts.UI;
using JMGames.Scripts.UI.Window;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JMGames.Scripts.Managers
{
    public class UIManager : JMBehaviour
    {
        public InteractionText InteractionText;
        public static UIManager Instance;
        public Texture2D CursorTexture;
        public CursorMode CursorMode = CursorMode.Auto;
        public Vector2 HotSpot = Vector2.zero;
        public NotificationText NotificationText;

        public override void DoStart()
        {
            Instance = this;
            //Cursor.SetCursor(CursorTexture, HotSpot, CursorMode);
            base.DoStart();
        }

        public void OnApplicationFocus(bool focus)
        {
            if (!focus && PauseMenuWindow.Instance != null)
            {
                Cursor.visible = true;
                PauseMenuWindow.Instance.Pause();
            }
        }

        public void OnApplicationPause(bool pause)
        {
#if !UNITY_EDITOR
            if (pause)
            {
                Cursor.visible = true;
                PauseMenuWindow.Instance.Pause();
            }
#endif
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

        public bool HasActiveWindows(UIWindowID excludedWindow)
        {
            List<UIWindow> windows = UIWindow.GetWindows();
            foreach (UIWindow window in windows)
            {
                if (window.ID != excludedWindow && window.IsVisible)
                {
                    return true;
                }
            }

            return false;
        }

        public void ShowNotification(string title, string text)
        {
            NotificationText.ShowNotification(title, text);
        }

        public void HideWindow(UIWindow window)
        {
            window.Hide();
            if (!UIManager.Instance.HasActiveWindows(window.ID))
            {
                UIManager.Instance.SetCursorVisibility(false);
            }
        }

        public void OnWindowStateChanged(UIWindow window, UIWindow.VisualState state)
        {
            if (state == UIWindow.VisualState.Hidden)
            {
                if (!UIManager.Instance.HasActiveWindows(window.ID))
                {
                    UIManager.Instance.SetCursorVisibility(false);
                }
            }
        }
    }
}

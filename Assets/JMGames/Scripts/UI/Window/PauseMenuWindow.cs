using DuloGames.UI;
using JMGames.Framework;
using JMGames.Common.Entities;
using JMGames.Scripts.Managers;
using UnityEngine;

namespace JMGames.Scripts.UI.Window
{
    public class PauseMenuWindow : JMBehaviour
    {
        public UIWindow ExitPromptModal;
        private UIWindow window;
        public static PauseMenuWindow Instance;
        public override void DoStart()
        {
            window = GetComponent<UIWindow>();
            Instance = this;
            base.DoStart();
        }

        public override void DoUpdate()
        {
            if (UIWindowManager.Instance != null && UIWindowManager.Instance.escapedUsed)
            {
                if (!window.IsOpen)
                {
                    Resume();
                }
            }
            base.DoUpdate();
        }

        public void Resume()
        {
            Time.timeScale = 1;
            GameStateManager.Instance.CurrentState = GameStateEnum.Playing;
            if (!UIManager.Instance.HasActiveWindows(window.ID))
            {
                UIManager.Instance.SetCursorVisibility(false);
            }
            if (window.IsOpen)
            {
                window.Toggle();
            }
        }

        public void Pause()
        {
            GameStateManager.Instance.CurrentState = GameStateEnum.Paused;
            Time.timeScale = 0;
            if (!window.IsOpen)
            {
                window.Toggle();
            }
        }

        public void Exit()
        {
            Application.Quit();
        }

        public void CancelExit()
        {
            ExitPromptModal.gameObject.SetActive(false);
        }

        public void PromptExit()
        {
            ExitPromptModal.gameObject.SetActive(true);
            ExitPromptModal.Toggle();
        }
    }
}

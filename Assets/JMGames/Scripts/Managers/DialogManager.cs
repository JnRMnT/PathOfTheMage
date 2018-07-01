using DuloGames.UI;
using JMGames.Framework;
using JMGames.JMDialogs.Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using UIDialogs = JMGames.Scripts.UI.Dialogs;
using UnityEngine;
using JMGames.Common.Enums;

namespace JMGames.Scripts.Managers
{
    public class DialogManager : JMBehaviour
    {
        public Transform DialogItemWindow;
        public static DialogManager Instance;
        public Dialog ActiveDialog;
        public int StepIndex;
        public delegate void DialogEndedDelegate(Dialog dialog, DialogEndReasonType endReason);
        public event DialogEndedDelegate OnDialogEnded;

        public override void DoStart()
        {
            if (DialogItemWindow == null)
            {
                UIWindow dialogWindow = UIWindow.GetWindow(UIWindowID.Dialog);
                DialogItemWindow = dialogWindow.transform;
            }
            Instance = this;
            ActiveDialog = null;
            base.DoStart();
        }

        public static void StartDialog(Dialog dialog)
        {
            if (Instance.ActiveDialog != null)
            {
                return;
            }
            Instance.ActiveDialog = dialog;
            Instance.StepIndex = 0;
            Instance.StartDialogItem(dialog.Items[0]);
        }

        private void StartDialogItem(DialogItem dialogItem)
        {
            UIDialogs.DialogItem uiDialogItem = Instance.DialogItemWindow.GetComponent<UIDialogs.DialogItem>();
            uiDialogItem.Type = dialogItem.Type;
            uiDialogItem.Initialize(dialogItem);
            Instance.DialogItemWindow.gameObject.SetActive(true);
            Instance.DialogItemWindow.GetComponent<UIWindow>().Show();
        }

        public void Next()
        {
            if (ActiveDialog != null)
            {
                if (StepIndex + 1 < ActiveDialog.Items.Count)
                {
                    StepIndex++;
                    StartDialogItem(ActiveDialog.Items[StepIndex]);
                }
                else
                {
                    DialogEnded(DialogEndReasonType.Completed);
                    UIManager.Instance.HideWindow(DialogItemWindow.GetComponent<UIWindow>());
                }
            }
        }

        public void DialogItemStateChanged(UIWindow window, UIWindow.VisualState state)
        {
            if (state == UIWindow.VisualState.Hidden)
            {
                DialogEnded(DialogEndReasonType.Canceled);
            }
        }

        private void DialogEnded(DialogEndReasonType endReasonType)
        {
            if (OnDialogEnded != null)
            {
                OnDialogEnded(ActiveDialog, endReasonType);
            }
            ActiveDialog = null;
        }
    }
}

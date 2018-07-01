using JMGames.Framework;
using JMGames.JMDialogs.Infrastructure.Base;
using JMGames.Scripts.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseDialogs = JMGames.JMDialogs.Infrastructure.Base;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DuloGames.UI;

namespace JMGames.Scripts.UI.Dialogs.ContentInitializers
{
    public class ResponseItemsInitializer : JMBehaviour
    {
        public GameObject ResponseButtonPrefab;
        private DialogItem boundItem;

        public void Initialize(DialogItem item)
        {
            boundItem = item;
            List<DialogResponseItem> availableResponses = item.Definition.Item.AvailableResponses;
            if (availableResponses != null && availableResponses.Count > 0)
            {
                foreach (BaseDialogs.DialogResponseItem responseItem in availableResponses)
                {
                    GameObject responseItemObject = GameObjectManager.InstantiatePrefab(ResponseButtonPrefab, transform, Entities.GameObjectTypeEnum.DialogResponseItem);
                    Text responseText = responseItemObject.transform.GetComponentInChildren<Text>();
                    Button responseButton = responseItemObject.GetComponent<Button>();
                    responseButton.onClick.AddListener(() => { HandleResponse(responseItem); });
                    responseText.text = responseItem.Content;
                }
            }
        }

        private void HandleResponse(BaseDialogs.DialogResponseItem responseItem)
        {
            UIManager.Instance.HideWindow(boundItem.GetComponent<UIWindow>());
        }
    }
}

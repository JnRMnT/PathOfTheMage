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
using JMGames.Common.Entities;
using JMGames.Scripts.Utilities;

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
                int index = 0;
                List<Transform> children = GameObjectUtilities.GetAllChildren(transform);
                foreach (BaseDialogs.DialogResponseItem responseItem in availableResponses)
                {
                    GameObject responseItemObject = null;
                    if (children.Count > index)
                    {
                        responseItemObject = children[index].gameObject;
                    }
                    else
                    {
                        responseItemObject = GameObjectManager.InstantiatePrefab(ResponseButtonPrefab, transform, GameObjectTypeEnum.DialogResponseItem);
                    }
                    Text responseText = responseItemObject.transform.GetComponentInChildren<Text>();
                    Button responseButton = responseItemObject.GetComponent<Button>();
                    responseButton.onClick.AddListener(() => { HandleResponse(responseItem); });
                    responseText.text = responseItem.Content;
                    responseItemObject.SetActive(true);
                    index++;
                }

                if (index < children.Count)
                {
                    for(int i = index; i < children.Count; i++)
                    {
                        children[i].gameObject.SetActive(false);
                    }
                }
            }
        }

        private void HandleResponse(BaseDialogs.DialogResponseItem responseItem)
        {
            DialogManager.Instance.Next();
        }
    }
}

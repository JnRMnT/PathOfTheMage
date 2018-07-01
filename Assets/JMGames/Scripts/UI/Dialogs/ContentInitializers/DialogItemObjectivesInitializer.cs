using JMGames.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace JMGames.Scripts.UI.Dialogs.ContentInitializers
{
    public class DialogItemObjectivesInitializer: JMBehaviour
    {
        public GameObject ObjectiveItemPrefab;
        public void Initialize(DialogItem item)
        {
            if (true)
            {
                //TODO: Check if any objective is available if not, hide objectives panel
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(true);
            }
        }
    }
}

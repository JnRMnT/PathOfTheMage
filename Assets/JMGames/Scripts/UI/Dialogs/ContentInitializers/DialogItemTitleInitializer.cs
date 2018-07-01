using JMGames.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;

namespace JMGames.Scripts.UI.Dialogs.ContentInitializers
{
    public class DialogItemTitleInitializer : JMBehaviour
    {

        public void Initialize(DialogItem item)
        {
            if (!string.IsNullOrEmpty(item.Definition.Item.Title))
            {
                GetComponent<Text>().text = item.Definition.Item.Title;
                transform.parent.gameObject.SetActive(true);
            }
            else
            {
                transform.parent.gameObject.SetActive(false);
            }
        }
    }
}

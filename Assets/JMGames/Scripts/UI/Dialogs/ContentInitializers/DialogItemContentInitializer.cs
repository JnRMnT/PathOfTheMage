using JMGames.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;

namespace JMGames.Scripts.UI.Dialogs.ContentInitializers
{
    public class DialogItemContentInitializer : JMBehaviour
    {
        public void Initialize(DialogItem item)
        {
            GetComponent<Text>().text = item.Definition.Item.Content;
        }
    }
}

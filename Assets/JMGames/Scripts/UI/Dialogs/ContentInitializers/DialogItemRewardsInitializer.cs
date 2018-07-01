using JMGames.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace JMGames.Scripts.UI.Dialogs.ContentInitializers
{
    public class DialogItemRewardsInitializer : JMBehaviour
    {
        public GameObject RewardItemPrefab;
        public void Initialize(DialogItem item)
        {
            if (true)
            {
                //TODO: Check if any reward is available if not, hide rewards panel
                transform.parent.gameObject.SetActive(false);
            }
        }
    }
}

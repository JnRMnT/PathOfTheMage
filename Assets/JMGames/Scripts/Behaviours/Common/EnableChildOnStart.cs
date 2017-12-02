using JMGames.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JMGames.Scripts.Behaviours.Common
{
    public class EnableChildOnStart: JMBehaviour
    {
        public override void DoStart()
        {
            transform.GetChild(0).gameObject.SetActive(true);
            base.DoStart();
        }
    }
}

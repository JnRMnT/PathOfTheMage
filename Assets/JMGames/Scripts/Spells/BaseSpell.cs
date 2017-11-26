using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace JMGames.Scripts.Spells
{
    public class BaseSpell
    {
        public virtual string Name
        {
            get
            {
                return string.Empty;
            }
        }

        public virtual string AnimationStateName
        {
            get
            {
                return string.Empty;
            }
        }

        public virtual GameObject Prefab
        {
            get
            {
                return null;
            }
        }
    }
}

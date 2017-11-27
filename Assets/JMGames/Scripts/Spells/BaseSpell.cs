using JMGames.Framework;
using JMGames.Scripts.Behaviours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace JMGames.Scripts.Spells
{
    public class BaseSpell : JMBehaviour
    {
        #region Properties
        public virtual string Name
        {
            get
            {
                return string.Empty;
            }
        }

        public virtual string[] AnimationTriggerNames
        {
            get
            {
                return null;
            }
        }

        public virtual float Damage
        {
            get
            {
                return 0f;
            }
        }

        public virtual GameObject Prefab
        {
            get
            {
                return gameObject;
            }
        }

        public Sprite Thumbnail
        {
            get
            {
                return null;
            }
        }
        #endregion


        public void HandleCollision(RaycastHit hit)
        {
            HitReceiver hitReceiver = hit.transform.GetComponent<HitReceiver>();
            if (hitReceiver != null)
            {
                hitReceiver.ReceiveHit(Damage);
            }
        }

    }
}

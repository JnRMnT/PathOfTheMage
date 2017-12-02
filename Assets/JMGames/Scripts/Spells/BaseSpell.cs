using JMGames.Framework;
using JMGames.Scripts.Behaviours;
using JMGames.Scripts.Spells.Effects;
using JMGames.Scripts.Utilities;
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

        public virtual float ManaCost
        {
            get
            {
                return 0f;
            }
        }

        public virtual string[] AnimationTriggerNames
        {
            get
            {
                return null;
            }
        }

        public virtual bool RotationNeeded
        {
            get
            {
                return true;
            }
        }

        public virtual Vector3 CharacterRotationOffset
        {
            get
            {
                return new Vector3(0, 20f, 0);
            }
        }

        public virtual SpellTypeEnum Type
        {
            get
            {
                return SpellTypeEnum.LinearCasting;
            }
        }

        public virtual AOETypeEnum AOEType
        {
            get
            {
                return AOETypeEnum.Undefined;
            }
        }

        public virtual float AOERadius
        {
            get
            {
                return 5f;
            }
        }

        public virtual float MaxCastDistance
        {
            get
            {
                return 20f;
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

        public virtual Sprite Thumbnail
        {
            get
            {
                return null;
            }
        }

        public virtual Type Effect
        {
            get
            {
                return null;
            }
        }
        #endregion
        
        public virtual void HandleCollision(object sender, RFX4_TransformMotion.RFX4_CollisionInfo e)
        {
            HitReceiver hitReceiver = e.Hit.transform.GetComponent<HitReceiver>();
            if (hitReceiver != null)
            {
                HitInfo hitInfo = GetHitInfo();
                if(hitInfo.RelativeHitPoint.x == -2 && hitInfo.RelativeHitPoint.y == -2 && hitInfo.RelativeHitPoint.z == -2)
                {
                    //Calculate hit point
                    hitInfo.RelativeHitPoint = GameObjectUtilities.FindPointRelativeToObject(e.Hit.point, e.Hit.transform.gameObject);
                }
                hitReceiver.ReceiveHit(hitInfo);
            }
        }

        protected virtual HitInfo GetHitInfo()
        {
            return new HitInfo
            {
                BaseDamage = Damage,
                RelativeHitPoint = new Vector3(-2, -2, -2)
            };
        }
    }
}

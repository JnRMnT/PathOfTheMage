using JMGames.Scripts.Spells.Effects;
using System;
using System.Collections;
using UnityEngine;

namespace JMGames.Scripts.Spells
{
    public class FireDragon : AOESpell
    {
        public override string[] AnimationTriggerNames
        {
            get
            {
                return new string[] { "CastSpell2H1" };
            }
        }


        public override float ManaCost
        {
            get
            {
                return 100f;
            }
        }

        public override bool RotationNeeded
        {
            get
            {
                return false;
            }
        }

        public override string Name
        {
            get
            {
                return "FireDragon";
            }
        }
        
        public override AOETypeEnum AOEType
        {
            get
            {
                return  AOETypeEnum.SelectedArea;
            }
        }

        public override float AOERadius
        {
            get
            {
                return 2.5f;
            }
        }

        public override Type[] Effects
        {
            get
            {
                return new Type[] { typeof(FireEffect) };
            }
        }

        public override float Damage
        {
            get
            {
                return 500f;
            }
        }

        public override int UIID
        {
            get
            {
                return 3;
            }
        }

        public override void DoStart()
        {
            StartCoroutine(WaitForDragonToFall());
            base.DoStart();
        }

        protected IEnumerator WaitForDragonToFall()
        {
            yield return new WaitForSeconds(5.6f);
            GiveDamage();
        }
    }
}
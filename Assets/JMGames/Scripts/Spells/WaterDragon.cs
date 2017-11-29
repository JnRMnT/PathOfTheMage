using JMGames.Scripts.Spells.Effects;
using System;
using System.Collections;
using UnityEngine;

namespace JMGames.Scripts.Spells
{
    public class WaterDragon : AOESpell
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
                return "WaterDragon";
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
                return 1f;
            }
        }

        public override Type Effect
        {
            get
            {
                return typeof(SlowEffect);
            }
        }

        public override float Damage
        {
            get
            {
                return 50f;
            }
        }

        public override void DoStart()
        {
            StartCoroutine(WaitForDragonToFall());
            base.DoStart();
        }

        protected IEnumerator WaitForDragonToFall()
        {
            yield return new WaitForSeconds(3.75f);
            GiveDamage();
        }
    }
}
using JMGames.Scripts.Behaviours;
using UnityEngine;

namespace JMGames.Scripts.Spells
{
    public class AOESpell : BaseSpell
    {
        public override SpellTypeEnum Type
        {
            get
            {
                return SpellTypeEnum.AOE;
            }
        }

        public void GiveDamage()
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position + Vector3.up * 0.1f, AOERadius);
            if (hitColliders != null)
            {
                foreach (Collider hitCollider in hitColliders)
                {
                    if (hitCollider.tag == "Enemy")
                    {
                        HitReceiver hitReceiver = hitCollider.GetComponent<HitReceiver>();
                        if(hitReceiver != null)
                        {
                            hitReceiver.ReceiveHit(GetHitInfo());
                        }
                    }
                }
            }
        }
    }
}

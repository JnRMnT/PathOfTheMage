using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JMGames.Scripts.Spells.Effects
{
    public class SpellEffect
    {
        public virtual string EffectName
        {
            get
            {
                return string.Empty;
            }
        }

        public virtual SpellEffectTiming Timing
        {
            get
            {
                return SpellEffectTiming.After;
            }
        }
    }
}

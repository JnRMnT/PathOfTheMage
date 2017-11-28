using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JMGames.Scripts.Utilities
{
    public class RaycastingUtilities
    {
        public static int CreateLayerMask(bool aExclude, params int[] aLayers)
        {
            int v = 0;
            foreach (var L in aLayers)
                v |= 1 << L;
            if (aExclude)
                v = ~v;
            return v;
        }
    }
}

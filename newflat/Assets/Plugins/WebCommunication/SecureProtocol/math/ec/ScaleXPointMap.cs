/*








daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

#if !BESTHTTP_DISABLE_ALTERNATE_SSL && (!UNITY_WEBGL || UNITY_EDITOR)

using System;

namespace Org.BouncyCastle.Math.EC
{
    public class ScaleXPointMap
        : ECPointMap
    {
        protected readonly ECFieldElement scale;

        public ScaleXPointMap(ECFieldElement scale)
        {
            this.scale = scale;
        }

        public virtual ECPoint Map(ECPoint p)
        {
            return p.ScaleX(scale);
        }
    }
}

#endif

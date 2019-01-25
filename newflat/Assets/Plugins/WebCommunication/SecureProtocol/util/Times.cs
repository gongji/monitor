/*








daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

#if !BESTHTTP_DISABLE_ALTERNATE_SSL && (!UNITY_WEBGL || UNITY_EDITOR)

using System;

namespace Org.BouncyCastle.Utilities
{
    public sealed class Times
    {
        private static long NanosecondsPerTick = 100L;

        public static long NanoTime()
        {
            return DateTime.UtcNow.Ticks * NanosecondsPerTick;
        }
    }
}

#endif

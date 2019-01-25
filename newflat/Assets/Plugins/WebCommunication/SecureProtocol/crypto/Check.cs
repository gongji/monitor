/*








daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

#if !BESTHTTP_DISABLE_ALTERNATE_SSL && (!UNITY_WEBGL || UNITY_EDITOR)

using System;

namespace Org.BouncyCastle.Crypto
{
    internal class Check
    {
        internal static void DataLength(bool condition, string msg)
        {
            if (condition)
                throw new DataLengthException(msg);
        }

        internal static void DataLength(byte[] buf, int off, int len, string msg)
        {
            if (off + len > buf.Length)
                throw new DataLengthException(msg);
        }

        internal static void OutputLength(byte[] buf, int off, int len, string msg)
        {
            if (off + len > buf.Length)
                throw new OutputLengthException(msg);
        }
    }
}

#endif

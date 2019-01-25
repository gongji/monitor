/*








daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

#if !BESTHTTP_DISABLE_ALTERNATE_SSL && (!UNITY_WEBGL || UNITY_EDITOR)

using System;
using System.IO;

namespace Org.BouncyCastle.Crypto.Tls
{
    public interface TlsCipher
    {
        int GetPlaintextLimit(int ciphertextLimit);

        /// <exception cref="IOException"></exception>
        byte[] EncodePlaintext(long seqNo, byte type, byte[] plaintext, int offset, int len);

        /// <exception cref="IOException"></exception>
        byte[] DecodeCiphertext(long seqNo, byte type, byte[] ciphertext, int offset, int len);
    }
}

#endif

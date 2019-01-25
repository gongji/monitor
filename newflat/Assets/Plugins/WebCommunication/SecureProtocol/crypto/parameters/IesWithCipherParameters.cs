/*








daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

#if !BESTHTTP_DISABLE_ALTERNATE_SSL && (!UNITY_WEBGL || UNITY_EDITOR)

using System;

namespace Org.BouncyCastle.Crypto.Parameters
{
    public class IesWithCipherParameters : IesParameters
    {
        private int cipherKeySize;

        /**
         * @param derivation the derivation parameter for the KDF function.
         * @param encoding the encoding parameter for the KDF function.
         * @param macKeySize the size of the MAC key (in bits).
         * @param cipherKeySize the size of the associated Cipher key (in bits).
         */
        public IesWithCipherParameters(
            byte[]  derivation,
            byte[]  encoding,
            int     macKeySize,
            int     cipherKeySize) : base(derivation, encoding, macKeySize)
        {
            this.cipherKeySize = cipherKeySize;
        }

        public int CipherKeySize
        {
            get
            {
                return cipherKeySize;
            }
        }
    }

}

#endif

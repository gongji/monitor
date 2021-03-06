/*








daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

#if !BESTHTTP_DISABLE_ALTERNATE_SSL && (!UNITY_WEBGL || UNITY_EDITOR)

using System;

namespace Org.BouncyCastle.Crypto.Tls
{
    /// <summary>RFC 4492 5.4. (Errata ID: 2389)</summary>
    public abstract class ECBasisType
    {
        public const byte ec_basis_trinomial = 1;
        public const byte ec_basis_pentanomial = 2;

        public static bool IsValid(byte ecBasisType)
        {
            return ecBasisType >= ec_basis_trinomial && ecBasisType <= ec_basis_pentanomial;
        }
    }
}

#endif

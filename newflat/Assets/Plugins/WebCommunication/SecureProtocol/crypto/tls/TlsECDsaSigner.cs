/*








daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

#if !BESTHTTP_DISABLE_ALTERNATE_SSL && (!UNITY_WEBGL || UNITY_EDITOR)

using System;

using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Signers;

namespace Org.BouncyCastle.Crypto.Tls
{
    public class TlsECDsaSigner
        :   TlsDsaSigner
    {
        public override bool IsValidPublicKey(AsymmetricKeyParameter publicKey)
        {
            return publicKey is ECPublicKeyParameters;
        }

        protected override IDsa CreateDsaImpl(byte hashAlgorithm)
        {
            return new ECDsaSigner(new HMacDsaKCalculator(TlsUtilities.CreateHash(hashAlgorithm)));
        }

        protected override byte SignatureAlgorithm
        {
            get { return Tls.SignatureAlgorithm.ecdsa; }
        }
    }
}

#endif

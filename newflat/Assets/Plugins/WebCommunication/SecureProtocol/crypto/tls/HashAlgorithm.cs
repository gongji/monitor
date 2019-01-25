/*








daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

#if !BESTHTTP_DISABLE_ALTERNATE_SSL && (!UNITY_WEBGL || UNITY_EDITOR)

using System;

namespace Org.BouncyCastle.Crypto.Tls
{
    /// <summary>RFC 5246 7.4.1.4.1</summary>
    public abstract class HashAlgorithm
    {
        public const byte none = 0;
        public const byte md5 = 1;
        public const byte sha1 = 2;
        public const byte sha224 = 3;
        public const byte sha256 = 4;
        public const byte sha384 = 5;
        public const byte sha512 = 6;
    }
}

#endif
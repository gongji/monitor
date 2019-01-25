/*








daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

#if !BESTHTTP_DISABLE_ALTERNATE_SSL && (!UNITY_WEBGL || UNITY_EDITOR)

using System;

using Org.BouncyCastle.Crypto;

namespace Org.BouncyCastle.Crypto.Parameters
{

    /// <summary> Cipher parameters with a fixed salt value associated with them.</summary>
    public class ParametersWithSalt : ICipherParameters
    {
        private byte[] salt;
        private ICipherParameters parameters;

        public ParametersWithSalt(ICipherParameters parameters, byte[] salt):this(parameters, salt, 0, salt.Length)
        {
        }

        public ParametersWithSalt(ICipherParameters parameters, byte[] salt, int saltOff, int saltLen)
        {
            this.salt = new byte[saltLen];
            this.parameters = parameters;

            Array.Copy(salt, saltOff, this.salt, 0, saltLen);
        }

        public byte[] GetSalt()
        {
            return salt;
        }

        public ICipherParameters Parameters
        {
            get
            {
                return parameters;
            }
        }
    }
}

#endif

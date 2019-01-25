/*








daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

#if !BESTHTTP_DISABLE_ALTERNATE_SSL && (!UNITY_WEBGL || UNITY_EDITOR)

using System;

namespace Org.BouncyCastle.Crypto
{
	/// <summary>
	/// This exception is thrown whenever a cipher requires a change of key, iv
	/// or similar after x amount of bytes enciphered
    /// </summary>
#if !(NETCF_1_0 || NETCF_2_0 || SILVERLIGHT || NETFX_CORE)
    [Serializable]
#endif
    public class MaxBytesExceededException
		: CryptoException
	{
		public MaxBytesExceededException()
		{
		}

		public MaxBytesExceededException(
			string message)
			: base(message)
		{
		}

		public MaxBytesExceededException(
			string		message,
			Exception	e)
			: base(message, e)
		{
		}
	}
}

#endif

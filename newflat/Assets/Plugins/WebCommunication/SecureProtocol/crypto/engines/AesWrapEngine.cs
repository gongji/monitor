/*








daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

#if !BESTHTTP_DISABLE_ALTERNATE_SSL && (!UNITY_WEBGL || UNITY_EDITOR)

namespace Org.BouncyCastle.Crypto.Engines
{
	/// <remarks>
	/// An implementation of the AES Key Wrapper from the NIST Key Wrap Specification.
	/// <p/>
	/// For further details see: <a href="http://csrc.nist.gov/encryption/kms/key-wrap.pdf">http://csrc.nist.gov/encryption/kms/key-wrap.pdf</a>.
	/// </remarks>
	public class AesWrapEngine
		: Rfc3394WrapEngine
	{
		public AesWrapEngine()
			: base(new AesEngine())
		{
		}
	}
}

#endif

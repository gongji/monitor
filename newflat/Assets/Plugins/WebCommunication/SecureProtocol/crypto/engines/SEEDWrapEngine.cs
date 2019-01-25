/*








daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

#if !BESTHTTP_DISABLE_ALTERNATE_SSL && (!UNITY_WEBGL || UNITY_EDITOR)

namespace Org.BouncyCastle.Crypto.Engines
{
	/// <remarks>
	/// An implementation of the SEED key wrapper based on RFC 4010/RFC 3394.
	/// <p/>
	/// For further details see: <a href="http://www.ietf.org/rfc/rfc4010.txt">http://www.ietf.org/rfc/rfc4010.txt</a>.
	/// </remarks>
	public class SeedWrapEngine
		: Rfc3394WrapEngine
	{
		public SeedWrapEngine()
			: base(new SeedEngine())
		{
		}
	}
}

#endif

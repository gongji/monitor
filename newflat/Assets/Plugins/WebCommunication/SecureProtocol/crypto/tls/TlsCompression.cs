/*








daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

#if !BESTHTTP_DISABLE_ALTERNATE_SSL && (!UNITY_WEBGL || UNITY_EDITOR)

using System;
using System.IO;

namespace Org.BouncyCastle.Crypto.Tls
{
	public interface TlsCompression
	{
		Stream Compress(Stream output);

		Stream Decompress(Stream output);
	}
}

#endif

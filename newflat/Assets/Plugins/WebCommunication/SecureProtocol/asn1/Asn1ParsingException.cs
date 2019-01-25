/*








daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

#if !BESTHTTP_DISABLE_ALTERNATE_SSL && (!UNITY_WEBGL || UNITY_EDITOR)
using System;

namespace Org.BouncyCastle.Asn1
{
#if !(NETCF_1_0 || NETCF_2_0 || SILVERLIGHT || NETFX_CORE)
    [Serializable]
#endif
    public class Asn1ParsingException
		: InvalidOperationException
	{
		public Asn1ParsingException()
			: base()
		{
		}

		public Asn1ParsingException(
			string message)
			: base(message)
		{
		}

		public Asn1ParsingException(
			string		message,
			Exception	exception)
			: base(message, exception)
		{
		}
	}
}

#endif
